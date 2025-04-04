using DataDomain;
using LogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit.Primitives;

namespace PresentationLayer.Views
{
    /// <summary>
    /// Interaction logic for AuditView.xaml
    /// </summary>
    public partial class AuditView : Page
    {
        public AuditManager auditManager = new AuditManager();
        public Audit selectedAudit;
        List<Audit> Audits { get; set; }
        public UserVM accessToken;
        public AuditView(UserVM userVM)
        {
            accessToken = userVM;
            InitializeComponent();
        }

        private void dgAudits_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Audits = auditManager.GetAllAudits();
                dgAudits.ItemsSource = Audits;

            }
            catch (Exception ex)
            {
                string message = ex.Message;
                if (ex.InnerException != null)
                {
                    message += "\n\n" + ex.InnerException.Message;
                }
                System.Windows.MessageBox.Show(message, "Audit Retrieval Failed");
            }
        }

        private void btnDeleteAudit_Click(object sender, RoutedEventArgs e)
        {
            ViewAudit viewAudit = new ViewAudit();
            viewAudit.Show();
        }
    }
}
