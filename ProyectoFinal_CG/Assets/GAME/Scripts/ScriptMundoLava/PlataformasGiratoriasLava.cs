using UnityEngine;

public class PlataformasGiratoriasLava : MonoBehaviour

    {
        [Header("Rotación")]
        public float velocidad = 40f;
        public Vector3 eje = new Vector3(0, 1, 0); 

        void Update()
        {
            transform.Rotate(eje * velocidad * Time.deltaTime);
        }
    }


