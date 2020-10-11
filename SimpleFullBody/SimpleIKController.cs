using UnityEngine;

public class SimpleIKController : MonoBehaviour {
    [SerializeField]
    Transform kafaHedefi, kafaBakis;

    [SerializeField]
    Transform solElHedef;

    [SerializeField]
    float solElMesafe = .5f;

    //Başlangıçta karakterin dik durumda kalması için 90 değerini veriyoruz.
    float kafaAci = 90;

    void LateUpdate () {

        float vertical = Input.GetAxis ("Mouse Y");

        //Fareden aldığımız veriyi kafaacimizi ekilyoruz.
        kafaAci += vertical;
        //Clamp fonksiyonu sayesinde açıyı 0 ve 180 arasında tutuyoruz. 
        kafaAci = Mathf.Clamp (kafaAci, 0, 180);

        /*
        sin ve cos fonksiyonlarından yararlanarak kafa için oluşturduğumuz Chain IK hedef pozisyonunu belirliyoruz.
        Bu fonksiyonlar sayesinde dairesel bir hareket oluşturabiliyoruz.
        localPosition değerine bunu atadığımızda da yörüngemiz parent konum olmuş oluyor.
        */
        Vector3 kafaPos = new Vector3 (0, Mathf.Sin (kafaAci * Mathf.Deg2Rad), Mathf.Cos (kafaAci * Mathf.Deg2Rad));
        kafaHedefi.transform.localPosition = kafaPos;

        /*
        Kafanın bakacağı yön, kafapos için oluşturduğumuz değere dik olacak şekildeydi. Derste bunu görebilirsiniz.
        Bunun için de 90 derece gerisinden gelmesi yeterli.
        Kafabakis  değerini 5 ile çarpma sebebimiz, objenin çok yakınlaşıp elin pozisyonunu bozmasını engellemek.
        */
        Vector3 kafaBakisPos = new Vector3 (0, Mathf.Sin ((kafaAci - 90) * Mathf.Deg2Rad), Mathf.Cos ((kafaAci - 90) * Mathf.Deg2Rad));
        kafaBakis.transform.localPosition = kafaBakisPos * 5f;

        /*
        Kamera ile bakacağımız obje arasına elimizi yerleştiriyoruz. 
        daha önce kafabakis değerini 5 ile çarptığımız için, burada bölerek daha sağlıklı bir kontrol sağlıyoruz.
        Son olarak da lookAt fonksiyonundan faydalanıp bakılması gereken noktaya elimizi döndürüyoruz.
        */
        solElHedef.position = Vector3.Lerp (Camera.main.transform.position, kafaBakis.position, solElMesafe / 5f);
        solElHedef.LookAt (kafaBakis.position);
    }
}
