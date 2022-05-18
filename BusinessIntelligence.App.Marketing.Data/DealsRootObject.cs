using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessIntelligence.App.Marketing
{
    public class Partner
    {
        public string description { get; set; }
        public string name { get; set; }
        public string neighborhood { get; set; }
        public string address { get; set; }
        public string number { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip_code { get; set; }
        public string phone_number { get; set; }
        public string website { get; set; }
        public string formatted_url { get; set; }
        public string image { get; set; }
        public string company_name { get; set; }
        public string cnpj { get; set; }
        public string canonical_url { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Image
    {
        public string image { get; set; }
    }

    public class BuyingOption
    {
        public string buying_option_id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public int available_units { get; set; }
        public double sale_price { get; set; }
        public double full_price { get; set; }
        public double percentage_saved { get; set; }
        public bool sold_out { get; set; }
    }

    public class Location
    {
        public string description { get; set; }
        public string name { get; set; }
        public string neighborhood { get; set; }
        public string address { get; set; }
        public string number { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip_code { get; set; }
        public string phone_number { get; set; }
        public string website { get; set; }
        public string formatted_url { get; set; }
        public string image { get; set; }
        public string company_name { get; set; }
        public string cnpj { get; set; }
        public string canonical_url { get; set; }
    }

    public class Deal
    {
        public string deal_id { get; set; }
        public string contract_number { get; set; }
        public string deal_type { get; set; }
        public string deal_category { get; set; }
        public string page_id { get; set; }
        public string title { get; set; }
        public string short_title { get; set; }
        public string ultra_brief_description { get; set; }
        public string highlights { get; set; }
        public string fine_print { get; set; }
        public string validity_message { get; set; }
        public string formatted_url { get; set; }
        public string legacy_formatted_url { get; set; }
        public int unified_discount_id { get; set; }
        public bool sold_out { get; set; }
        public string expiration_date { get; set; }
        public int available_units { get; set; }
        public Partner partner { get; set; }
        public List<Image> images { get; set; }
        public List<BuyingOption> buying_options { get; set; }
        public string deal_city { get; set; }
        public string canonical_url { get; set; }
        public string shortened_url { get; set; }
        public double full_price { get; set; }
        public double sale_price { get; set; }
        public string percentage_saved { get; set; }
        public int sold_units_by_contract { get; set; }
        public int remaining_units { get; set; }
        public bool one_year_promotion { get; set; }
        public List<Location> locations { get; set; }
    }

    public class Response
    {
        public List<Deal> deals { get; set; }
        public bool hasMore { get; set; }
    }

    public class DealsRootObject
    {
        public int code { get; set; }
        public Response response { get; set; }
    }
}
