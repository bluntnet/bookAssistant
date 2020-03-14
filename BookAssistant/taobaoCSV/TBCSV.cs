using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace BookAssistant.taobaoCSV
{
    class TBCSV
    {
        private static Dictionary<string, TBColumn> csvDict = null;
        private static List<TBColumn> columnlist = new List<TBColumn>(
            new TBColumn[]{
               new TBColumn("title", "宝贝名称","宝贝名称",true), 
               new TBColumn("cid","宝贝类目","50000093",false),
               new TBColumn("seller_cids","店铺类目","97589456",true),
                new TBColumn("stuff_status","新旧程度","1",false),
                new TBColumn("location_state","省","北京",true), 
                new TBColumn("location_city" ,"城市","北京",true),
                new TBColumn("item_type","出售方式","1",false),
                new TBColumn("price" ,"宝贝价格","100",false),
                new TBColumn("auction_increment" ,"加价幅度","0",true),
                new TBColumn("num" ,"宝贝数量","3",false),
                new TBColumn("valid_thru" ,"有效期","7",false),
                new TBColumn("freight_payer","运费承担","2",false),
                new TBColumn("post_fee" ,"平邮","",false),
                new TBColumn("ems_fee" ,"EMS","",false),
                new TBColumn("express_fee" ,"快递","0",false),
                new TBColumn("has_invoice" ,"发票","0",false),
                new TBColumn("has_warranty" ,"保修","0",false),
                new TBColumn("approve_status","放入仓库","0",false),
                new TBColumn("has_showcase","橱窗推荐","0",false),
                new TBColumn("list_time","开始时间","",true),
                new TBColumn("description","宝贝描述","",true),
                new TBColumn("cateProps","宝贝属性","",true),
                new TBColumn("postage_id","邮费模版ID","2698291",false),
                new TBColumn("has_discount","会员打折","1",false),
                new TBColumn("modified","修改时间","",true),
                new TBColumn("upload_fail_msg","上传状态","",true),
                new TBColumn("picture_status","图片状态","100",true),
                new TBColumn("auction_point","返点比例","0",false),
                new TBColumn("picture","新图片","",true),
                new TBColumn("video","视频","",true),
                new TBColumn("skuProps","销售属性组合","",true),
                new TBColumn("inputPids","用户输入ID串","",true),
                new TBColumn("inputValues","用户输入名-值对","",true),
                new TBColumn("outer_id","商家编码","",true),
                new TBColumn("propAlias","销售属性别名","",true),
                new TBColumn("auto_fill","代充类型","0",false),
                new TBColumn("num_id","数字ID","0",false),
                new TBColumn("local_cid","本地ID","-1",false),
                new TBColumn("navigation_type","宝贝分类","",false),
                new TBColumn("user_name","用户名称","yihui1973",false),
                new TBColumn("syncStatus","宝贝状态","1",false),
                new TBColumn("is_lighting_consigment","闪电发货","208",false),
                new TBColumn("is_xinpin","新品","248",false),
                new TBColumn("foodparame","食品专项","",true),
                new TBColumn("features","尺码库","",false),
                new TBColumn("buyareatype","采购地","0",false),
                new TBColumn("global_stock_type","库存类型","-1",false),
                new TBColumn("global_stock_country","国家地区","",false),
                new TBColumn("sub_stock_type","库存计数","1",true),
                new TBColumn("item_size","物流体积","",false),
                new TBColumn("item_weight","物流重量","",false),
                new TBColumn("sell_promise","退换货承诺","0",true),
                new TBColumn("custom_design_flag","定制工具","",true),
                new TBColumn("wireless_desc","无线详情","",true),
                new TBColumn("barcode","商品条形码","",false),
                new TBColumn("sku_barcode","sku 条形码","",false),
                new TBColumn("newprepay","7天退货","",false),
                new TBColumn("subtitle","宝贝卖点","",false),
                new TBColumn("cpv_memo","属性值备注","",false),
                new TBColumn("input_custom_cpv","自定义属性值","",false),
                new TBColumn("qualification","商品资质","",false),
                new TBColumn("add_qualification","增加商品资质","",false),
                new TBColumn("o2o_bind_service","关联线下服务","",false)
            }
            );

        public static Dictionary<string, TBColumn> getCSVDict()
        {
            if (csvDict != null) return csvDict;
            csvDict = new Dictionary<string, TBColumn>();
            foreach (TBColumn column in columnlist)
            {
                csvDict.Add(column.Key, column);
            }
            return csvDict;
        }

        public DataTable getDefaultDataTable()
        {
            Dictionary<string, TBColumn> dict = getCSVDict();
            Dictionary<string, TBColumn>.KeyCollection keys = dict.Keys;
            DataTable exportDT = new DataTable();
            DataRow drhead1 = exportDT.NewRow();
            DataRow drhead2 = exportDT.NewRow();
            int i = 0;
            foreach (TBColumn column in columnlist)
            {
                exportDT.Columns.Add(column.Key);
                drhead1[i] = column.Key;
                drhead2[i] = column.Name;
                i++;
            }
            exportDT.Rows.Add(drhead1);
            exportDT.Rows.Add(drhead2);
            return exportDT;
        }
        public static string[] getTaobaoCSVHead()
        {
            Dictionary<string, TBColumn> dict = getCSVDict();

            int count = columnlist.Count;
            string[] head = new string[count];
            int i = 0;
            foreach (TBColumn value in columnlist)
            {
                head[i++] = value.Name;
            }
            return head;
        }

        public static string[] getTaobaoDefaultValue()
        {
            Dictionary<string, TBColumn> dict = getCSVDict();

            int count = columnlist.Count;
            string[] head = new string[count];
            int i = 0;
            foreach (TBColumn value in columnlist)
            {
                head[i++] = value.DefaultValue;
            }
            return head;
        }

        internal static int[] getQuoteMark()
        {
            Dictionary<string, TBColumn> dict = getCSVDict();
            int count = columnlist.Count;
            int[] quoteMark = new int[count];
            int i = 0;
            foreach (TBColumn value in columnlist)
            {
                quoteMark[i++] = value.QuoteMark ? 1 : 0;
            }
            return quoteMark;
        }
    }
}
