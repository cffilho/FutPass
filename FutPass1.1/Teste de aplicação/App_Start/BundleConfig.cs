using System.Web;
using System.Web.Optimization;

namespace Teste_de_aplicação
{
    public class BundleConfig
    {
        // Para obter mais informações sobre o agrupamento, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/sb-admin-2").Include(
                        "~/Scripts/sb-admin-2.js"));

            bundles.Add(new ScriptBundle("~/bundles/sb-admin-2.min.js").Include(
                        "~/Scripts/sb-admin-2.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery.easing.min.js").Include(
                        "~/Content/vendor/jquery-easing/jquery.easing.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap.bundle.min.js").Include(
                        "~/Content/vendor/bootstrap/js/bootstrap.bundle.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery.dataTables.js").Include(
                        "~/Content/vendor/datatables/jquery.dataTables.js"));

            bundles.Add(new ScriptBundle("~/bundles/tabledefault.js").Include(
                        "~/Content/vendor/datatables/tabledefault.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery.min.js").Include(
                       "~/Content/vendor/jquery/jquery.min.js"));

            // Use a versão em desenvolvimento do Modernizr para desenvolver e aprender. Em seguida, quando estiver
            // pronto para a produção, utilize a ferramenta de build em https://modernizr.com para escolher somente os testes que precisa.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/alerts.js").Include(
                      "~/Scripts/alerts.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/bundles/all.min.css").Include(
                      "~/Content/vendor/fontawesome-free/css/all.min.css"));

            bundles.Add(new StyleBundle("~/bundles/sb-admin-2.css").Include(
                      "~/Content/sb-admin-2.css"));
        }
    }
}
