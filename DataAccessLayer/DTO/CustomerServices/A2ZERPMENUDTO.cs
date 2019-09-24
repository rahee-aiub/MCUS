using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.DTO.CustomerServices
{
    public class A2ZERPMENUDTO
    {
        public Int16 UserId { set; get; }
        public Int16 ModuleNo { set; get; }
        public Int16 MenuNo { set; get; }
        public String MenuName { set; get; }
        public Int16 MenuParentNo { set; get; }
        public String MenuUrl { set; get; }
        public Int16 MenuLogFlag { set; get; }
    }
}
