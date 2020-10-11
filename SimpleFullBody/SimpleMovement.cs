using UnityEngine;

public class SimpleMovement : MonoBehaviour {
    Rigidbody rigidbody;

    void Start () {
        rigidbody = GetComponent<Rigidbody> ();
        //Kuvvet durumlarında yön değişimi olmaması için rotasyon işlemlerini donduruyoruz.
        rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }
    void FixedUpdate () {

        float vertical = Input.GetAxis ("Vertical");
        float horizontal = Input.GetAxis ("Horizontal");

        //TranformVector ile oluşturduğumuz Vector3 tipini local hale getiriyoruz, bu sayede karakter nereye dönerse hareket yönü de ona göre güncellenecek
        rigidbody.velocity = transform.TransformVector (new Vector3 (horizontal, 0, vertical));
    }

    void LateUpdate () {
        //Farenin sağ sol değerlerini okuyup bu değerlerle Y ekseninde karakteri yönetiyoruz.
        float m_horizontal = Input.GetAxis ("Mouse X");
        transform.Rotate (Vector3.up * m_horizontal);
    }

}
