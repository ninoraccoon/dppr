using pr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Core.Models;
using Umbraco.Web.Mvc;
using umbraco;
using Umbraco.Web;

namespace pr.Controllers
{
    public class SiteLayoutController : SurfaceController
    {
        public const string PARTIALS_HOME_FOLDER = "~/Views/Partials/SiteLayout/";
        // GET: SiteLayout
        public ActionResult RenderHeader()
        {
            var homenode = Services.ContentService.GetById(getHomeNode(CurrentPage));
            var menuP = homenode.Children().First(o => o.ContentType.Alias == "principal");
            var menuItemP = menuP.Children();
            var menupmodel = new List<menuitem>();
            menupmodel.Add(new menuitem() {
                titulo = homenode.GetValue<string>("titulo"),
                url = new UmbracoHelper(UmbracoContext).Url(homenode.Id)
            });
            menuItemP.ToList().ForEach(o => {
                menupmodel.Add(new menuitem() {
                    titulo = o.GetValue<string>("titulo"),
                    url = new UmbracoHelper(UmbracoContext).Url(o.Id)
                });
            });            
            var model = new HeaderModel() {
                Titulo = homenode.GetValue<string>("titulo")
            };
            model.principal = menupmodel;
            
            return PartialView(PARTIALS_HOME_FOLDER + "_header.cshtml", model);
        }
        public ActionResult RenderFooter()
        {
            return PartialView(PARTIALS_HOME_FOLDER + "_footer.cshtml");
        }
        //metodo helper
        public static int getHomeNode(IPublishedContent CurrentPage)
        {
            return int.Parse(CurrentPage.Path.Split(',')[1]);           
           
        }

    }
}