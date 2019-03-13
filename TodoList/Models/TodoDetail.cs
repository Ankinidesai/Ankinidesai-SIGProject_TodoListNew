using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TodoList.Models
{
    public class TodoDetail
    {
        public int ID { get; set; }

        public string Description { get; set; }

        public bool Isdone { get; set; }

        public virtual ApplicationUser User { get; set; }

        public bool IsParent { get; set; }

        public int ParentID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DeadlineDate { get; set; }

    }
}