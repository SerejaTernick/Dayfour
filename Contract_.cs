//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Dayfour
{
    using System;
    using System.Collections.Generic;
    
    public partial class Contract_
    {
        public int IDContract { get; set; }
        public Nullable<double> NumberAccount { get; set; }
        public Nullable<int> IDUser { get; set; }
        public Nullable<double> Amount { get; set; }
        public Nullable<int> Period { get; set; }
        public Nullable<System.DateTime> ExpirationDate { get; set; }
        public Nullable<double> Percet { get; set; }
    
        public virtual BankAccount_ BankAccount_ { get; set; }
        public virtual User_ User_ { get; set; }
    }
}
