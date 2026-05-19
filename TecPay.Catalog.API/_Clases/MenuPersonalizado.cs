using TecPay.Catalog.Application.Common;

namespace TecPay.Catalog.API._Clases
{
    public static class MenuPersonalizado
    {
        public static List<Menu> GetListaMenu()
        {
            List<Menu> menus = new List<Menu>();

            Menu menu;
            List<SubMenu> subMenus;
            SubMenu subMenu;

            #region "RISO"

            menu = new Menu();
            menu.Titulo = "Tecpay";
            menu.Icono = "mdi mdi-folder-lock-open";

            subMenus = new List<SubMenu>();

            subMenu = new SubMenu();
            subMenu.Titulo = "Producto";
            subMenu.URL = "producto";
            subMenus.Add(subMenu);

            subMenu = new SubMenu();
            subMenu.Titulo = "Categoría";
            subMenu.URL = "categoria";
            subMenus.Add(subMenu);

            
            menu.SubMenu = subMenus;

            menus.Add(menu);

            #endregion // FIN RISO

            return menus;
        }
    }
}
