using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;
using W = DocumentFormat.OpenXml.Wordprocessing;

using Newtonsoft.Json;
using PowerPoint = Microsoft.Office.Interop.PowerPoint;
using Office = Microsoft.Office.Core;



namespace Agente_de_Automatización
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void btnInvestigar_Click(object sender, EventArgs e)
        {
            string tema = txtTema.Text.Trim();
            if (string.IsNullOrEmpty(tema))
            {
                MessageBox.Show("Por favor, ingrese un tema para investigar.");
                return;
            }

           rtbResultado.Text = "Consultando API de IA...";

            string resultado = await ConsultarApiIAAsync(txtTema.Text.Trim());

            rtbResultado.Text = resultado;
        }

        private async void btnGuardar_Click(object sender, EventArgs e)
        {
            string tema = txtTema.Text.Trim();
            string resultado = rtbResultado.Text;

            if (string.IsNullOrEmpty(resultado))
            {
                MessageBox.Show("No hay resultado para guardar.");
                return;
            }

            // Guardar en base de datos SQL Server
            await GuardarEnBaseDeDatosAsync(tema, resultado);

            // Generar Word y PowerPoint
            GenerarWord(resultado);
            GenerarPowerPoint(resultado, tema);

            MessageBox.Show("Documentos generados y guardados correctamente.");
        }
            
        private async Task<string> ConsultarApiIAAsync(string prompt)
        {
            string apiKey = "APIkey - Secreta";
            int maxReintentos = 3;

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var requestBody = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
            new { role = "user", content = prompt }
        }
            };

            string jsonBody = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            for (int intento = 1; intento <= maxReintentos; intento++)
            {
                try
                {
                    var response = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);

                    if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                    {
                        int delay = intento * 2000; // 2s, 4s, 6s...
                        await Task.Delay(delay);
                        continue;
                    }

                    response.EnsureSuccessStatusCode();

                    string responseContent = await response.Content.ReadAsStringAsync();
                    dynamic json = JsonConvert.DeserializeObject(responseContent);
                    return (string)json.choices[0].message.content;
                }
                catch (HttpRequestException ex)
                {
                    if (intento == maxReintentos)
                        return $"❌ Error después de varios intentos: {ex.Message}";
                }
            }

            return "⚠️ No se pudo obtener respuesta tras varios intentos.";
        }

        private async Task GuardarEnBaseDeDatosAsync(string prompt, string resultado)
        {
            string connectionString = "Server=TERETA-PC\\SQLEXPRESS;Database=DB_Investigaciones;Trusted_Connection=True;";
            string query = "INSERT INTO Investigaciones (Prompt, Resultado, Fecha) VALUES (@Prompt, @Resultado, GETDATE())";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new(query, conn))
            {
                cmd.Parameters.AddWithValue("@Prompt", prompt);
                cmd.Parameters.AddWithValue("@Resultado", resultado);
                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }            
        }

        private void GenerarWord(string contenido)
        {
            string carpeta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Investigaciones");
            Directory.CreateDirectory(carpeta);
            string ruta = Path.Combine(carpeta, "Investigacion.docx");

            using (WordprocessingDocument wordDoc = WordprocessingDocument.Create(ruta, DocumentFormat.OpenXml.WordprocessingDocumentType.Document))
            {
                MainDocumentPart mainPart = wordDoc.AddMainDocumentPart();
                mainPart.Document = new W.Document();
                W.Body body = mainPart.Document.AppendChild(new W.Body());
                                
                string tema = txtTema.Text.Trim();
                W.Paragraph titulo = new W.Paragraph(
                    new W.ParagraphProperties(
                        new W.Justification() { Val = W.JustificationValues.Center },
                        new W.SpacingBetweenLines() { After = "200" }
                    ),
                    new W.Run(
                        new W.RunProperties(
                            new W.Bold(),
                            new W.FontSize() { Val = "32" } // Tamaño 16pt (multiplicado por 2)
                        ),
                        new W.Text(tema)
                    )
                );
                body.AppendChild(titulo);

                // Separar el contenido en párrafos según saltos de línea
                var parrafos = contenido.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var parrafo in parrafos)
                {
                    W.Paragraph p = new W.Paragraph(
                        new W.Run(
                            new W.Text(parrafo)
                        )
                    );
                    body.AppendChild(p);
                }

                mainPart.Document.Save();
            }
        }

        private void GenerarPowerPoint(string contenido, string tema)
        {
            string carpeta = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Investigaciones");
            Directory.CreateDirectory(carpeta);
            string ruta = Path.Combine(carpeta, "Investigacion.pptx");

            var pptApp = new PowerPoint.Application();
            pptApp.Visible = Office.MsoTriState.msoTrue;

            var pres = pptApp.Presentations.Add(Office.MsoTriState.msoTrue);

            var partes = contenido.Split(new[] { "\r\n\r\n", "\n\n" }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < partes.Length; i++)
            {
                var slide = pres.Slides.Add(i + 1, PowerPoint.PpSlideLayout.ppLayoutText);

                if (i == 0)
                    slide.Shapes[1].TextFrame.TextRange.Text = tema; // título para diapositivas
                else
                    slide.Shapes[1].TextFrame.TextRange.Text = $"{tema} (cont.)";

                slide.Shapes[2].TextFrame.TextRange.Text = partes[i].Trim();
            }

            pres.SaveAs(ruta, PowerPoint.PpSaveAsFileType.ppSaveAsOpenXMLPresentation, Office.MsoTriState.msoTrue);

            pres.Close();
            pptApp.Quit();

            System.Runtime.InteropServices.Marshal.ReleaseComObject(pres);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(pptApp);
        }
    }
}
