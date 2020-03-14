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
               new TBColumn("title", "��������","��������",true), 
               new TBColumn("cid","������Ŀ","50000093",false),
               new TBColumn("seller_cids","������Ŀ","97589456",true),
                new TBColumn("stuff_status","�¾ɳ̶�","1",false),
                new TBColumn("location_state","ʡ","����",true), 
                new TBColumn("location_city" ,"����","����",true),
                new TBColumn("item_type","���۷�ʽ","1",false),
                new TBColumn("price" ,"�����۸�","100",false),
                new TBColumn("auction_increment" ,"�Ӽ۷���","0",true),
                new TBColumn("num" ,"��������","3",false),
                new TBColumn("valid_thru" ,"��Ч��","7",false),
                new TBColumn("freight_payer","�˷ѳе�","2",false),
                new TBColumn("post_fee" ,"ƽ��","",false),
                new TBColumn("ems_fee" ,"EMS","",false),
                new TBColumn("express_fee" ,"���","0",false),
                new TBColumn("has_invoice" ,"��Ʊ","0",false),
                new TBColumn("has_warranty" ,"����","0",false),
                new TBColumn("approve_status","����ֿ�","0",false),
                new TBColumn("has_showcase","�����Ƽ�","0",false),
                new TBColumn("list_time","��ʼʱ��","",true),
                new TBColumn("description","��������","",true),
                new TBColumn("cateProps","��������","",true),
                new TBColumn("postage_id","�ʷ�ģ��ID","2698291",false),
                new TBColumn("has_discount","��Ա����","1",false),
                new TBColumn("modified","�޸�ʱ��","",true),
                new TBColumn("upload_fail_msg","�ϴ�״̬","",true),
                new TBColumn("picture_status","ͼƬ״̬","100",true),
                new TBColumn("auction_point","�������","0",false),
                new TBColumn("picture","��ͼƬ","",true),
                new TBColumn("video","��Ƶ","",true),
                new TBColumn("skuProps","�����������","",true),
                new TBColumn("inputPids","�û�����ID��","",true),
                new TBColumn("inputValues","�û�������-ֵ��","",true),
                new TBColumn("outer_id","�̼ұ���","",true),
                new TBColumn("propAlias","�������Ա���","",true),
                new TBColumn("auto_fill","��������","0",false),
                new TBColumn("num_id","����ID","0",false),
                new TBColumn("local_cid","����ID","-1",false),
                new TBColumn("navigation_type","��������","",false),
                new TBColumn("user_name","�û�����","yihui1973",false),
                new TBColumn("syncStatus","����״̬","1",false),
                new TBColumn("is_lighting_consigment","���緢��","208",false),
                new TBColumn("is_xinpin","��Ʒ","248",false),
                new TBColumn("foodparame","ʳƷר��","",true),
                new TBColumn("features","�����","",false),
                new TBColumn("buyareatype","�ɹ���","0",false),
                new TBColumn("global_stock_type","�������","-1",false),
                new TBColumn("global_stock_country","���ҵ���","",false),
                new TBColumn("sub_stock_type","������","1",true),
                new TBColumn("item_size","�������","",false),
                new TBColumn("item_weight","��������","",false),
                new TBColumn("sell_promise","�˻�����ŵ","0",true),
                new TBColumn("custom_design_flag","���ƹ���","",true),
                new TBColumn("wireless_desc","��������","",true),
                new TBColumn("barcode","��Ʒ������","",false),
                new TBColumn("sku_barcode","sku ������","",false),
                new TBColumn("newprepay","7���˻�","",false),
                new TBColumn("subtitle","��������","",false),
                new TBColumn("cpv_memo","����ֵ��ע","",false),
                new TBColumn("input_custom_cpv","�Զ�������ֵ","",false),
                new TBColumn("qualification","��Ʒ����","",false),
                new TBColumn("add_qualification","������Ʒ����","",false),
                new TBColumn("o2o_bind_service","�������·���","",false)
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
