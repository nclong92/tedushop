using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Tedushop.Web.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="Vui lòng nhập tên")]
        public string FullName { get; set; }

        [Required(ErrorMessage ="Vui lòng nhập tên tài khoản.")]
        public string UserName { get; set; }

        [Required(ErrorMessage ="Vui lòng nhập mật khẩu.")]
        [MinLength(6, ErrorMessage ="Mật khẩu ít nhất 6 ký tự.")]
        public string Password { get; set; }

        [EmailAddress(ErrorMessage ="Thư điện tử không hợp lệ.")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Vui lòng nhập địa chỉ.")]
        public string Address { get; set; }

        [Required(ErrorMessage ="Vui lòng nhập số điện thoại.")]
        public string PhoneNumber { get; set; }
    }
}