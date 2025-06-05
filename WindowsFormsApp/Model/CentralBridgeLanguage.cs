using AccountCreatorForm.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp.Model
{
    public static class CentralBridgeLanguage
    {
        public static void Language()
        {
            Home.Instance.LoadLanguageHome();
            if (ViewChange.Instance != null)
            {
                ViewChange.Instance.LoadLanguageViewChange();
            }
            if (ScreenView.Instance != null)
            {
                ScreenView.Instance.LoadLanguageScreenView();
            }
            if (ViewThanhToan.Instance != null)
            {
                ViewThanhToan.Instance.LoadLanguageViewThanhToan();
            }
        }
    }
}
