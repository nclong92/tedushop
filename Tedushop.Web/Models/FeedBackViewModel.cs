using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tedushop.Web.Models
{
    public class FeedBackViewModel
    {
        [MaxLength(250, ErrorMessage ="Tên tối đa 250 ký tự.")]
        [Required(ErrorMessage ="Vui lòng nhập tên")]
        public string Name { get; set; }

        [MaxLength(250, ErrorMessage ="Email tối đa 250 ký tự.")]
        public string Email { get; set; }

        [MaxLength(500, ErrorMessage ="Tin nhắn tối đa 500 ký tự.")]
        public string Message { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required(ErrorMessage ="Vui lòng nhập trạng thái.")]
        public bool Status { get; set; }


        public ContactDetailViewModel ContactDetailViewModel { get; set; }
    }
}