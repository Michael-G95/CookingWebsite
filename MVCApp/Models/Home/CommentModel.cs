using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MVCApp.Models
{
    public class CommentModel
    {

        // data class used on the Staff comment view page, and the Home about page to submit a comment
        // No data annotations - not user facing
        // Data annotations used - user facing class

        [Required(AllowEmptyStrings =false,ErrorMessage = "Please enter a name!")] // Field required for validation
        [Display(Name ="Submitter's Name")] // Setting user-facing name of the field
        public string CommentName { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter an email!")]
        [EmailAddress(ErrorMessage = "Please enter a valid email!")]
        [Display(Name = "Submitter's Email")]
        public string CommentEmail { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a subject!")]
        [Display(Name = "Comment Subject")]
        public string CommentSubject { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a comment!")]
        [Display(Name = "Comment")]
        [MaxLength(255)]
        public string CommentMessage { get; set; }

        public int State { get; set; }
    }
}