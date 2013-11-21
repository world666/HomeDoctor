using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeDoctor.Models
{
    public enum ManageMessageId
    {
        ChangePasswordSuccess,
        SetPasswordSuccess,
        RemoveLoginSuccess,
        Error
    }

    public enum ProfileMessageId
    {
        ChangeProfileSuccess,
        Error
    }
}