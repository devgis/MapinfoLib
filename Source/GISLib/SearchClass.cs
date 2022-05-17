using System;
using System.Collections.Generic;
using System.Text;
using MapInfo.Data;
using MapInfo.Mapping;
using System.Reflection;
using System.IO;
using MapInfo.Windows.Controls;
using System.Windows.Forms;
using MapInfo.Geometry;

namespace GISLib
{
    /// <summary>
    /// 查询类
    /// </summary>
    public class SearchClass
    {
        #region Table实例
        //省级行政区地图名称
        private Table ProviceTbl;
        //市级行政区地图名称
        private Table CityTbl;
        //镇级行政区地图名称
        private Table TownTbl;

        Map map;
        MapControl mControl;
        CoordSys cs;
        #endregion

        #region 单例实例
        private static SearchClass instance;
        /// <summary>
        /// 单例实例
        /// </summary>
        public static SearchClass Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SearchClass();
                    instance.InitMemMap();
                }
                return instance;
            }
        }
        #endregion

        #region 共有方法
        /// <summary>
        /// 调用此方法创建对象不做任何其他操作
        /// </summary>
        public void Init()
        { }


        /// <summary>
        /// 查询信息点
        /// </summary>
        /// <param name="x">经度</param>
        /// <param name="y">纬度</param>
        /// <param name="distance">距离 单位米</param>
        /// <returns></returns>
        public String SearchNearRoad(double x, double y,double distance)
        {
            Point pSearchPoint = new Point(map.GetDisplayCoordSys(), x, y);
            //查找省

            String RoadName = string.Empty;
            SearchInfo si = MapInfo.Data.SearchInfoFactory.SearchNearest(pSearchPoint,new Distance(distance,DistanceUnit.Meter));
            si.QueryDefinition.Columns = null;
            Table tmpTable = (map.Layers["高速"] as FeatureLayer).Table;
            IResultSetFeatureCollection ifs = MapInfo.Engine.Session.Current.Catalog.Search(tmpTable, si);
            if (ifs.Count > 0)
            {
                RoadName = ifs[0]["Name"].ToString();
            }
            return RoadName;

        }

        static IResultSetFeatureCollection ifs = null;
        /// <summary>
        /// 查询信息点
        /// </summary>
        /// <param name="x">经度</param>
        /// <param name="y">纬度</param>
        /// <returns></returns>
        public String SearchNearRoad(double x, double y)
        {
            Point pSearchPoint = new Point(cs, x, y);
            //查找省

            String RoadName = string.Empty;
            //SearchInfo si = MapInfo.Data.SearchInfoFactory.SearchNearest(pSearchPoint);
            SearchInfo si = MapInfo.Data.SearchInfoFactory.SearchNearest(pSearchPoint, new Distance(10000, DistanceUnit.Meter));
            si.QueryDefinition.Columns = null;
            Table tmpTable = (map.Layers["ROAD"] as FeatureLayer).Table;
            ifs = MapInfo.Engine.Session.Current.Catalog.Search(tmpTable, si);
            if (ifs!=null&&ifs.Count > 0)
            {
                RoadName = ifs[0]["Name"].ToString();
            }
            return RoadName;

        }
        ///// <summary>
        ///// 默认1000米内最近的道路
        ///// </summary>
        ///// <param name="x">经度</param>
        ///// <param name="y">纬度</param>
        ///// <returns></returns>
        //public String SearchNearRoad(double x, double y)
        //{
        //    double distance=1000;
        //    return SearchNearRoad(x,y,distance);
        //}
        #endregion


        #region 私有方法
        /// <summary>
        /// 初始化地图数据到内存
        /// </summary>
        private void InitMemMap()
        {
            mControl=new MapControl();
            //String mwsPath = Path.Combine(Application.StartupPath, "Map/map.mws");

            String mwsPath = System.Configuration.ConfigurationManager.AppSettings["mappath"]; //@"E:\Temp\Code006\Realease\Map\map.mws";
            MapInfo.Mapping.MapTableLoader tLoader = new MapTableLoader();
            map = mControl.Map;
            MapWorkSpaceLoader mwsLoader = new MapWorkSpaceLoader(mwsPath);
            map.Load(mwsLoader);
            cs = map.GetDisplayCoordSys();
        }
        #endregion
    }
}
