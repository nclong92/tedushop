using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tedushop.Web.Models
{
    public class ContactDetailViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage ="Vui lòng nhập tên.")]
        public string Name { get; set; }

        [MaxLength(50, ErrorMessage ="Số điện thoại không vượt quá 250.")]
        public string Phone { get; set; }

        [MaxLength(250, ErrorMessage = "Thư điện tử không vượt quá 250.")]
        public string Email { get; set; }

        [MaxLength(250, ErrorMessage = "Website không vượt quá 250.")]
        public string Website { get; set; }

        [MaxLength(250, ErrorMessage = "Địa chỉ không vượt quá 250.")]
        public string Address { get; set; }

        [MaxLength(250, ErrorMessage = "Thông tin khác không vượt quá 250.")]
        public string Other { get; set; }

        public double? Lat { get; set; }
        public double? Lng { get; set; }
        public bool Status { get; set; }
    }
}