using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication3

{
    class geneldegisken
    {

        public static string kitap_id = "";
        public static bool kitap_kaydedildi=false;
        public void kitap_id_al(string id){
            kitap_id=id;
        }
        public string kitap_id_ver() {

            return kitap_id;
        }
        public void kitap_id_sifirla() {
            kitap_id = "";
        }

        public void kayit_yapildi() {
            kitap_kaydedildi = true;
        }
        public bool kayit_yapildimi() {
            return kitap_kaydedildi;
        }
        public void kayit_yenilendi()
        {
            kitap_kaydedildi = false;
        }

             
    }
}
