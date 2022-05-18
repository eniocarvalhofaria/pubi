using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessIntelligence.Util
{
  public  class EmbeddedResource
    {

        public static string TextResource(System.Reflection.Assembly Assembly, string ResourceName)
        {
            try
            {
                System.IO.StreamReader a = new System.IO.StreamReader(Resource(Assembly, ResourceName), System.Text.Encoding.Default);
                string retorno = a.ReadToEnd();
                a.Close();
                return retorno;
            }
            catch (Exception ex)
            {
                return null;
          //      throw new System.Exception( "O RECURSO DE TEXTO :" + ResourceName + " NAO E UM RECURSO VALIDO",ex);
            }
        }

        public static System.IO.Stream Resource( string AssemblyName,  string ResourceName)
        {
            System.Reflection.Assembly C = System.Reflection.Assembly.LoadFile(AssemblyName);
            return Resource( C,  ResourceName);
        }
        public static System.IO.Stream Resource( System.Reflection.Assembly Assembly,  string ResourceName)
        {
            foreach (String n in Assembly.GetManifestResourceNames())
            {
                if (n.IndexOf(ResourceName) > -1)
                {
                    return Assembly.GetManifestResourceStream(n);
                }
            
            }

            return null;
        }

    }
}
