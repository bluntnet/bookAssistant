
using System;
using System.Text;
using System.Collections;
using System.IO;
using System.Data;

namespace CsvLib
{
    #region ��˵����Ϣ
    /// <summary>
    ///  <DL>
    ///  <DT><b>дCSV�ļ���,���ȸ�CSV�ļ���ֵ,���ͨ��Save�������б������</b></DT>
    ///   <DD>
    ///    <UL> 
    ///    </UL>
    ///   </DD>
    ///  </DL>
    ///  <Author>yangzhihong</Author>    
    ///  <CreateDate>2006/01/16</CreateDate>
    ///  <Company></Company>
    ///  <Version>1.0</Version>
    /// </summary>
    #endregion
    public class CsvStreamWriter
    {
        private ArrayList rowAL;        //������,CSV�ļ���ÿһ�о���һ����
        private string fileName;       //�ļ���
        private Encoding encoding;       //����
        private int[] quoteMark;
        private int firstRowQuote = 0; //��һ��û�ж���
        public CsvStreamWriter()
        {
            this.rowAL = new ArrayList();
            this.fileName = "";
            this.encoding = Encoding.Default;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName">�ļ���,�����ļ�·��</param>
        public CsvStreamWriter(string fileName)
        {
            this.rowAL = new ArrayList();
            this.fileName = fileName;
            this.encoding = Encoding.Default;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName">�ļ���,�����ļ�·��</param>
        /// <param name="encoding">�ļ�����</param>
        public CsvStreamWriter(string fileName, Encoding encoding)
        {
            this.rowAL = new ArrayList();
            this.fileName = fileName;
            this.encoding = encoding;
        }

        /// <summary>
        /// row:��,row = 1�����һ��
        /// col:��,col = 1�����һ��
        /// </summary>
        public string this[int row, int col]
        {
            set
            {
                //���н����ж�
                if (row <= 0)
                {
                    throw new Exception("��������С��0");
                }
                else if (row > this.rowAL.Count) //�����ǰ����������������Ҫ����
                {
                    for (int i = this.rowAL.Count + 1; i <= row; i++)
                    {
                        this.rowAL.Add(new ArrayList());
                    }
                }
                else
                {
                }
                //���н����ж�
                if (col <= 0)
                {
                    throw new Exception("��������С��0");
                }
                else
                {
                    ArrayList colTempAL = (ArrayList)this.rowAL[row - 1];

                    //���󳤶�
                    if (col > colTempAL.Count)
                    {
                        for (int i = colTempAL.Count; i <= col; i++)
                        {
                            colTempAL.Add("");
                        }
                    }
                    this.rowAL[row - 1] = colTempAL;
                }
                //��ֵ
                ArrayList colAL = (ArrayList)this.rowAL[row - 1];

                colAL[col - 1] = value;
                this.rowAL[row - 1] = colAL;
            }
        }


        /// <summary>
        /// �ļ���,�����ļ�·��
        /// </summary>
        public string FileName
        {
            set
            {
                this.fileName = value;
            }
        }
        public int FirstRowQuote
        {
            set{
                this.firstRowQuote = value;
            }
        }

        /// <summary>
        /// �ļ�����
        /// </summary>

        public Encoding FileEncoding
        {
            set
            {
                this.encoding = value;
            }
        }

        public void QuoteMark(int[] value)
        { 
            
            this.quoteMark=value;
            
        }
        /// <summary>
        /// ��ȡ��ǰ�����
        /// </summary>
        public int CurMaxRow
        {
            get
            {
                return this.rowAL.Count;
            }
        }

        /// <summary>
        /// ��ȡ�����
        /// </summary>
        public int CurMaxCol
        {
            get
            {
                int maxCol;

                maxCol = 0;
                for (int i = 0; i < this.rowAL.Count; i++)
                {
                    ArrayList colAL = (ArrayList)this.rowAL[i];

                    maxCol = (maxCol > colAL.Count) ? maxCol : colAL.Count;
                }

                return maxCol;
            }
        }

        /// <summary>
        /// ��ӱ����ݵ�CSV�ļ���
        /// </summary>
        /// <param name="dataDT">������</param>
        /// <param name="beginCol">�ӵڼ��п�ʼ,beginCol = 1�����һ��</param>
        public void AddData(DataTable dataDT, int beginCol)
        {
            if (dataDT == null)
            {
                throw new Exception("��Ҫ��ӵı�����Ϊ��");
            }
            int curMaxRow;

            curMaxRow = this.rowAL.Count;
            for (int i = 0; i < dataDT.Rows.Count; i++)
            {
                for (int j = 0; j < dataDT.Columns.Count; j++)
                {
                    this[curMaxRow + i + 1, beginCol + j] = dataDT.Rows[i][j].ToString();
                }
            }
        }

        /// <summary>
        /// ��������,�����ǰӲ�����Ѿ������ļ���һ�����ļ������Ḳ��
        /// </summary>
        public void Save()
        {
            //�����ݵ���Ч�Խ����ж�
            if (this.fileName == null)
            {
                throw new Exception("ȱ���ļ���");
            }
            else if (File.Exists(this.fileName))
            {
                File.Delete(this.fileName);
            }
            if (this.encoding == null)
            {
                this.encoding = Encoding.Default;
            }
            System.IO.StreamWriter sw = new StreamWriter(this.fileName, false, this.encoding);

            sw.WriteLine("version 1.00");
            for (int i = 0; i < this.rowAL.Count; i++)
            {
                sw.WriteLine(ConvertToSaveLine((ArrayList)this.rowAL[i],i));
                
            }

            sw.Close();
        }

        /// <summary>
        /// ��������,�����ǰӲ�����Ѿ������ļ���һ�����ļ������Ḳ��
        /// </summary>
        /// <param name="fileName">�ļ���,�����ļ�·��</param>
        public void Save(string fileName)
        {
            this.fileName = fileName;
            Save();
        }

        /// <summary>
        /// ��������,�����ǰӲ�����Ѿ������ļ���һ�����ļ������Ḳ��
        /// </summary>
        /// <param name="fileName">�ļ���,�����ļ�·��</param>
        /// <param name="encoding">�ļ�����</param>
        public void Save(string fileName, Encoding encoding)
        {
            this.fileName = fileName;
            this.encoding = encoding;
            Save();
        }


        /// <summary>
        /// ת���ɱ�����
        /// </summary>
        /// <param name="colAL">һ��</param>
        /// <returns></returns>
        private string ConvertToSaveLine(ArrayList colAL,int row)
        {
            string saveLine;

            saveLine = "";
            for (int i = 0; i < colAL.Count; i++)
            {

                if (row < firstRowQuote)
                {
                    saveLine += ConvertToSaveCell(colAL[i].ToString());
                }
                else {
                    if (quoteMark != null && i < quoteMark.Length && quoteMark[i] > 0)
                    {
                        saveLine += addQuoteMark(ConvertToSaveCell(colAL[i].ToString()));
                    }
                    else
                    {
                        saveLine += ConvertToSaveCell(colAL[i].ToString());
                    }
                }
                if (i < colAL.Count - 1)
                {
                    saveLine += "\t";
                }
            }

            return saveLine;
        }

        /// <summary>
        /// �ַ���ת����CSV�еĸ���
        /// ˫����ת��������˫���ţ�Ȼ����β����һ��˫����
        /// �����Ͳ���Ҫ���Ƕ��ż����е�����
        /// </summary>
        /// <param name="cell">��������</param>
        /// <returns></returns>
        private string ConvertToSaveCell(string cell)
        {
            cell = cell.Replace("\"", "\"\"");
            cell = cell.Replace("\r\n","");
            return cell;
        }
        private string addQuoteMark(String cell)
        {
            return "\"" + cell + "\"";
        }
    }
}
