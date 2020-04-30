using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Xml.Linq;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;

namespace AltfErp
{
    class MetodosSql
    {
        
        public static string Host { get; set; }
        public static string Port { get; set; }
        public static string Instance { get; set; }
        public static string User { get; set; }
        public static string Password { get; set; }
        public static string Timeout { get; set; }
        public static string Type { get; set; }

        public static MemoryStream memory;

        public static void SetParameters()
        {
            
            try
            {
                string path = String.Format(@"{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "config.xml");
                XElement xml = XElement.Load(path.Replace(@"\", "\\"));


                Host = xml.Element("host").Value;
                Port = xml.Element("port").Value;
                Instance = xml.Element("instance").Value;
                User = xml.Element("user").Value;
                Password = xml.Element("password").Value;
                Timeout = xml.Element("timeout").Value;
                Type = xml.Element("type").Value;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

        }

        public static Boolean ChecaLogin(string usuario, string senha)
        {
            Boolean validaCadastro;
            string sql = String.Format(@"SELECT COUNT(USUARIO) AS CADASTRO FROM LOGIN WHERE USUARIO = '{0}' AND SENHA = '{1}'", usuario, senha);
            int numeroCad = int.Parse(GetField(sql, "CADASTRO"));
            if(numeroCad > 0)
            {
                validaCadastro = true;
            }
            else
            {
                validaCadastro = false;
            }

            return validaCadastro;
        }

        public static string GetConnectionString()
        {
            try
            {
                SetParameters();

                if (Type == "local")
                {
                    return String.Format(@"Data Source=.\{0};Initial Catalog={1};User ID={2}; Password={3};", Host, Instance, User, Password);
                }

                if (Type == "sqlExterno")
                {
                    return String.Format("Network Library=DBMSSOCN;Data Source={0},{1};Initial Catalog={2};User id={3};Password={4};Connection Timeout={5};", Host, Port, Instance, User, Password, Timeout);
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public static void InsereImagem(string sql, Bitmap _bmpImagem1, Bitmap _bmpImagem2)
        {
            

            Bitmap BmpImagem1, BmpImagem2;
            BmpImagem1 = _bmpImagem1;
            BmpImagem2 = _bmpImagem2;

            if(BmpImagem1 != null && BmpImagem2 != null)
            {
                SqlConnection con = new SqlConnection();
                SqlCommand cmd;

                MemoryStream memoryImagem1 = new MemoryStream();
                MemoryStream memoryImagem2 = new MemoryStream();

                BmpImagem1.Save(memoryImagem1, ImageFormat.Bmp);
                BmpImagem2.Save(memoryImagem2, ImageFormat.Bmp);

                byte[] foto1, foto2;
                foto1 = memoryImagem1.ToArray();
                foto2 = memoryImagem2.ToArray();

                con.ConnectionString = GetConnectionString();
                cmd = new SqlCommand(sql, con);

                SqlParameter imagem1 = new SqlParameter("@Imagem", SqlDbType.Binary);
                SqlParameter imagem2 = new SqlParameter("@Imagem2", SqlDbType.Binary);

                imagem1.Value = foto1;
                imagem2.Value = foto2;

                cmd.Parameters.Add(imagem1);
                cmd.Parameters.Add(imagem2);


                int i = 0;

                try
                {
                    con.Open();
                    i = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new System.Exception(ex.Message, ex);
                }
                finally
                {
                    con.Close();
                }
            }
            else if(BmpImagem1 == null && BmpImagem2 != null)
            {
                SqlConnection con = new SqlConnection();
                SqlCommand cmd;

                MemoryStream memoryImagem2 = new MemoryStream();


                BmpImagem2.Save(memoryImagem2, ImageFormat.Bmp);

                byte[] foto2;
                
                foto2 = memoryImagem2.ToArray();

                con.ConnectionString = GetConnectionString();
                cmd = new SqlCommand(sql, con);

                SqlParameter imagem2 = new SqlParameter("@Imagem2", SqlDbType.Binary);

                imagem2.Value = foto2;

                cmd.Parameters.Add(imagem2);

                int i = 0;

                try
                {
                    con.Open();
                    i = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new System.Exception(ex.Message, ex);
                }
                finally
                {
                    con.Close();
                }
            }
            else if(BmpImagem2 == null && BmpImagem1 != null)
            {
                SqlConnection con = new SqlConnection();
                SqlCommand cmd;

                MemoryStream memoryImagem1 = new MemoryStream();
               

                BmpImagem1.Save(memoryImagem1, ImageFormat.Bmp);


                byte[] foto1;
                foto1 = memoryImagem1.ToArray();
               

                con.ConnectionString = GetConnectionString();
                cmd = new SqlCommand(sql, con);

                SqlParameter imagem1 = new SqlParameter("@Imagem", SqlDbType.Binary);
                

                imagem1.Value = foto1;
                

                cmd.Parameters.Add(imagem1);
                
                int i = 0;

                try
                {
                    con.Open();
                    i = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new System.Exception(ex.Message, ex);
                }
                finally
                {
                    con.Close();
                }
            }


            
            
        }
                
               
                
                  
                



        public static void ExecQuery(string sql)
        {
            SqlConnection con = new SqlConnection();
            SqlCommand cmd;


            con.ConnectionString = GetConnectionString();
            cmd = new SqlCommand(sql, con);

            int i = 0;

            try
            {
                con.Open();
                i = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new System.Exception(ex.Message, ex);
            }
            finally
            {
                con.Close();
            }
        }

        public static Object ExecScalar(string sql)
        {
            SqlConnection con = new SqlConnection();
            SqlCommand cmd;

            con.ConnectionString = GetConnectionString();
            cmd = new SqlCommand(sql, con);

            try
            {
                con.Open();
                object i = cmd.ExecuteScalar();
                return i;
            }
            catch (Exception ex)
            {
                throw new System.Exception(ex.Message, ex);
            }
            finally
            {
                con.Close();
            }
        }

        public static MemoryStream GetImage(String sql, string field)
        {
            string valida;

            SqlConnection con = new SqlConnection();
            SqlCommand cmd;

            con.ConnectionString = GetConnectionString();

            try
            {
                
                cmd = new SqlCommand(sql, con);
                SqlDataReader dr;
                con.Open();
                dr = cmd.ExecuteReader();
                

                while(dr.Read())
                {
                    valida = dr[field].ToString();
                       
                    if(!String.IsNullOrWhiteSpace(valida.ToString()))
                    {
                        byte[] imagem = (byte[])(dr[field]);
                        memory = new MemoryStream(imagem);
                    }
                       
                }
                    
                    
                    
               
                
                


            }
            catch (Exception ex)
            {
                throw new System.Exception(ex.Message, ex);
            }
            finally
            {
                con.Close();
            }

            return memory;
        }

        public static string GetField(String sql, String field)
        {
            string retorno = string.Empty;

            SqlConnection con = new SqlConnection();
            SqlCommand cmd;

            con.ConnectionString = GetConnectionString();

            try
            {
                cmd = new SqlCommand(sql, con);
                SqlDataReader dr;
                con.Open();
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    retorno = Convert.ToString(String.Format("{0}", dr[field]));
                }
            }
            catch (Exception ex)
            {
                throw new System.Exception(ex.Message, ex);
            }
            finally
            {
                con.Close();
            }

            return retorno;
        }

        public static DataTable GetDT(String sql)
        {
            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd;

            con.ConnectionString = GetConnectionString();

            try
            {
                cmd = new SqlCommand(sql, con);
                con.Open();
                dt.Load(cmd.ExecuteReader());
                return dt;
            }
            catch (Exception ex)
            {
                throw new System.Exception(ex.Message, ex);
            }
            finally
            {
                con.Close();
            }
        }
    }
}
