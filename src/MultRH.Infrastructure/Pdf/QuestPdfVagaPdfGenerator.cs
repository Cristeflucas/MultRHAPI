using Microsoft.Extensions.Options;
using MultRH.Application.Vagas;
using MultRH.Application.Vagas.Dtos;
using MultRH.Infrastructure.Common;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Globalization;

namespace MultRH.Infrastructure.Pdf
{
    public class QuestPdfVagaPdfGenerator : IVagaPdfGenerator
    {
        private static readonly CultureInfo PtBr = new("pt-BR");
        private static readonly string LabelColor = Colors.Red.Darken1;
        private static readonly string HeadingColor = Colors.Blue.Darken2;

        private readonly byte[] _logoBytes;
        private readonly MultRhSettings _settings;

        public QuestPdfVagaPdfGenerator(IOptions<MultRhSettings> settings)
        {
            _settings = settings.Value;

            var logoPath = Path.Combine(AppContext.BaseDirectory, "Assets", "logo-mult.png");
            _logoBytes = File.ReadAllBytes(logoPath);
        }

        public byte[] Generate(VagaDto vaga)
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.DefaultTextStyle(x => x.FontSize(11));

                    page.Header().Column(header =>
                    {
                        header.Item().AlignCenter().Height(90).Image(_logoBytes).FitHeight();
                        header.Item().PaddingTop(10).AlignCenter().Text("CARTA DE ENCAMINHAMENTO PARA ENTREVISTAS")
                            .FontSize(16).Bold().FontColor(HeadingColor);
                    });

                    page.Content().PaddingTop(15).Column(column =>
                    {
                        column.Spacing(8);

                        column.Item().Text("ATENÇÃO: Só entra na empresa o candidato que a MULT RH agência de empregos enviou o nome para ser liberado a entrada para entrevista.")
                            .Bold().FontColor(HeadingColor);

                        column.Item().Text(text =>
                        {
                            text.Span("EMPRESA: ").Bold().FontColor(LabelColor);
                            text.Span(vaga.Empresa);
                        });

                        column.Item().PaddingTop(5).Text("INFORMAÇÕES IMPORTANTES:").Bold().FontColor(HeadingColor);
                        column.Item().Text("- Levar todos os documentos;");
                        column.Item().Text("- Levar Currículo impresso;");
                        column.Item().Text("- Levar a carteira de trabalho digital ou papel.");

                        column.Item().PaddingTop(5).Text(text =>
                        {
                            text.Span("FUNÇÃO: ").Bold().FontColor(LabelColor);
                            text.Span(vaga.Titulo);
                        });

                        column.Item().Text(text =>
                        {
                            text.Span("ENDEREÇO: ").Bold().FontColor(LabelColor);
                            text.Span(vaga.Endereco);
                        });

                        column.Item().Text(text =>
                        {
                            text.Span("PONTO DE REFERÊNCIA: ").Bold().FontColor(LabelColor);
                            text.Span(vaga.PontoRef);
                        });

                        column.Item().Text(text =>
                        {
                            text.Span("DATA DA ENTREVISTA: ").Bold().FontColor(LabelColor);
                            text.Span(vaga.DataEntrevista.HasValue
                                ? vaga.DataEntrevista.Value.ToString("dd 'DE' MMMM 'DE' yyyy", PtBr).ToUpper()
                                : "A DEFINIR");
                        });

                        column.Item().Text(text =>
                        {
                            text.Span("HORÁRIO DA ENTREVISTA: ").Bold().FontColor(LabelColor);
                            text.Span(vaga.HorarioEntrevista.HasValue
                                ? vaga.HorarioEntrevista.Value.ToString("HH:mm")
                                : "A DEFINIR");
                        });

                        column.Item().Text(text =>
                        {
                            text.Span("RESPONSÁVEL PELA ENTREVISTA: ").Bold().FontColor(LabelColor);
                            text.Span(vaga.ResponsavelEntrevista);
                        });

                        column.Item().PaddingTop(5).Text("OBSERVAÇÃO:").FontSize(14).Bold().FontColor(HeadingColor);
                        column.Item().Text("- A empresa só atenderá os candidatos que a MULT RH enviar para a entrevista.");

                        column.Item().PaddingTop(35).AlignCenter().Text(_settings.ResponsavelEncaminhamento)
                            .FontSize(13).Bold();
                        column.Item().AlignCenter().Text("...........................................................................").FontSize(9);
                        column.Item().AlignCenter().Text("Responsável pelo encaminhamento").FontSize(9);
                        column.Item().AlignCenter().Text("Mult Consultoria em RH").FontSize(9);
                    });

                    page.Footer().PaddingTop(10).BorderTop(1).BorderColor(Colors.Grey.Lighten1)
                        .PaddingTop(5).AlignCenter().Text(text =>
                        {
                            text.Span($"WhatsApp: {_settings.WhatsApp}    ").Bold();
                            text.Span($"Fone: {_settings.Telefone}    ").Bold();
                            text.Span($"Site: {_settings.Site}").Bold();
                        });
                });
            });

            return document.GeneratePdf();
        }
    }
}
