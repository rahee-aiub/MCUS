using System;

namespace DataAccessLayer.DTO.HouseKeeping
{
    public class A2ZERPMENUDTO
    {
        #region Properties

        public Int16 UserId { set; get; }
        public Int16 ModuleNo { set; get; }
        public Int16 MenuNo { set; get; }
        public String MenuName { set; get; }
        public Int16 MenuParentNo { set; get; }
        public String MenuUrl { set; get; }
        public Int16 MenuLogFlag { set; get; }


        #endregion
    }
}
