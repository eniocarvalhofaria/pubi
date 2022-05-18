using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessIntelligence.App.Marketing
{
    public abstract class DealTemplate : HtmlContent, ITemplate
    {
        public abstract string Description { get; }
        public virtual HtmlDeal Deal { get; set; }
        public override string GetHtml()
        {
            return GetHtml(Deal);
        }
        public abstract int DealsPerRow { get; }
        public abstract string OriginalTemplate { get; set; }
        public string GetHtml(HtmlDeal deal)
        {
            Deal = deal;
            string conteudo = OriginalTemplate;
            conteudo = NameReplacement(deal.Name, conteudo);
            conteudo = ShortNameReplacement(deal.ShortName, conteudo);
            conteudo = OriginalValueReplacement(deal.OriginalValue, conteudo);
            conteudo = ValueReplacement(deal.Value, conteudo);
            conteudo = ImageUrlReplacement(deal.ImageUrl, conteudo);
            conteudo = PartnerNameReplacement(deal.PartnerName, conteudo);
            //        conteudo = DealUrlReplacement(deal.DealUrl + @"?" + GetUrlCategory() + "utm_medium=" + deal.utm_medium + "&utm_campaign=" + deal.utm_campaign + "&utm_source=" + deal.utm_source, conteudo);
            //     conteudo = DealUrlReplacement(deal.DealUrl + @"?" + GetUrlCategory() + "utm_source=#utmSource#&utm_medium=#utmMedium#&utm_campaign=#utmCampaign#", conteudo);
            conteudo = DealUrlReplacement(deal.DealUrl, conteudo);
            return conteudo;

        }
        private string GetUrlCategory()
        {
            return "";
            switch (Deal.Category)
            {
                case "Gastronomia":
                    {
                        return "categoria=comer-e-beber&";
                    }
                case "Produtos":
                    {
                        return "categoria=produtos&";
                    }
                case "Comércio e Serviços Locais":
                    {
                        return "categoria=comercio-e-servicos-locais&";
                    }
                case "Viagens":
                    {
                        return "atributos=viagens&";
                    }
                case "Entretenimento, Cultura e Vida Noturna":
                    {
                        return "categoria=diversao&";
                    }
                case "Hotéis e Pousadas":
                    {
                        return "atributos=hoteis-e-pousadas&";
                    }
                case "Pacotes Internacionais":
                    {
                        return "categoria=pacotes-internacionais&";
                    }
                case "Pacotes Nacionais":
                    {
                        return "categoria=pacotes-nacionais&";
                    }
                case "Cruzeiros":
                    {
                        return "categoria=cruzeiros&";
                    }
                case "Resorts":
                    {
                        return "categoria=resorts&";
                    }
                case "BEBES (Beleza, Estética, Bem-Estar e Saúde)":
                    {
                        return "categoria=beleza-e-bem-estar&";
                    }
                case "Beleza & Saúde":
                    {
                        return "categoria=beleza-e-saude&";
                    }
                case "Esportes & Saúde":
                    {
                        return "categoria=esportes-e-saude&";
                    }
                case "Moda & Estilo":
                    {
                        return "categoria=moda-e-estilo&";
                    }
                case "Esportes & Lazer":
                    {
                        return "categoria=esportes-e-lazer&";
                    }

                case "Tecnologia & Informática":
                    {
                        return "categoria=tecnologia-e-informatica&";
                    }
                case "Ingressos":
                    {
                        return "categoria=ingressos&";
                    }
                case "Fotografia":
                    {
                        return "categoria=fotografia&";
                    }
                case "Casa":
                    {
                        return "categoria=casa&";
                    }
                case "Adulto":
                    {
                        return "categoria=adulto&";
                    }

                default:
                    {
                        return "";
                    }

            }


        }
        public virtual string NameReplacement(string name, string template)
        {
            return template.Replace("@Name", name);
        }
        public virtual string ShortNameReplacement(string shortName, string template)
        {
            return template.Replace("@ShortName", shortName);
        }
        public virtual string OriginalValueReplacement(string originalValue, string template)
        {
            if (Deal != null && Deal.IsFractionated)
            {
                return template.Replace("@OriginalValue", "a partir de");
            }
            else
            {
                return template.Replace("@OriginalValue", "<strike>" + originalValue + "</strike>");
            }
        }

        public virtual string ImageUrlReplacement(string imageUrl, string template)
        {
            return template.Replace("@ImageUrl", imageUrl);
        }
        public virtual string DealUrlReplacement(string dealUrl, string template)
        {
            return template.Replace("@DealUrl", dealUrl);
        }
        public virtual string PartnerNameReplacement(string partnerName, string template)
        {
            return template.Replace("@PartnerName", partnerName);
        }
        public virtual string ValueReplacement(string value, string template)
        {

            if (Deal == null)
            {
                return template;
            }
            else
            {
                return template.Replace("@Value", Deal.InitialValue.Replace("R$", ""));
            }

        }
    }
}
