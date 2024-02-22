
using System;

namespace HomeService.Models
{
    public partial class Configuration
    {
        public int ConfigurationId { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Facebook { get; set; }
        public string WhatsApp { get; set; }
        public string LinkedIn { get; set; }
        public string Instgram { get; set; }
        public string Twitter { get; set; }
        public Double? VisitCost { get; set; }
    }
}