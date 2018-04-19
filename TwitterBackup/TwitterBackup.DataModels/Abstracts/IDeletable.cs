using System;
using System.Collections.Generic;
using System.Text;

namespace TwitterBackup.Data.Models.Abstracts
{
    public interface IDeletable
    {
        bool isDeleted { get; set; }

        DateTime? DeletedOn { get; set; }
    }
}