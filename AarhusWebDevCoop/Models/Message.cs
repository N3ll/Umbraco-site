using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AarhusWebDevCoop.Models
{
    public class Message
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Text { get; set; }
    }
}