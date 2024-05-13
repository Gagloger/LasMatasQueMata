using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SistemaCombate : MonoBehaviour
{
    [SerializeField] private Transform puntoAtaque;
    [SerializeField] private Transform otroJugador;
    [SerializeField] private float multEmpuje;
    private float fuerzaEmpuje;
    private Vector2 direccionEmpuje;

    [SerializeField] private float vida;
    private float cargaAtaque;
    private bool cargandoAtaque;
    [Range(0f, 3.0f)][SerializeField] private float velocidadCarga;
    [SerializeField] private float radioAtaque;
    [SerializeField] private float radioAtaqueMax;

    private Animator animator;
    private Animator animatorAtks; [SerializeField] private GameObject esfera;

    private void Start() {
        cargaAtaque=0f;
        cargandoAtaque=false;
        animator = GetComponent<Animator>();
        animatorAtks = esfera.GetComponent<Animator>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.F) ){
            cargandoAtaque=true; 
            
        }

        if (Input.GetKeyUp(KeyCode.F)){
            switch (cargaAtaque){    //este es importante para las animaciones, pero mecanicamente no significa mucho
                case <0.5f:
                Debug.Log("Ataque debil");
                animatorAtks.SetTrigger("atkDebil");
                break;
                case <1f:
                Debug.Log("Ataque normal");
                animatorAtks.SetTrigger("atkMedio");
                break;
                case <2f:
                Debug.Log("Ataque fuerte");
                animatorAtks.SetTrigger("atkFuerte");
                break;
                default:
                Debug.Log("Ataque super");
                animatorAtks.SetTrigger("atkSuper");
                break;
            }
            Golpe(cargaAtaque*10);  //Cuando se lanza el ataque, segun la carga, hace mas o menos daño
            cargandoAtaque=false;
            vida-=(int)cargaAtaque*10; //*Time.deltaTime y dentro de la carga para que se pierda progresivamente
            cargaAtaque=0f;
            radioAtaque=0.5f; 
        }

        if  (cargandoAtaque && cargaAtaque<3){  //mientras se esta cargando el ataque = aumento de rango, aumento de empuje
            cargaAtaque += Time.deltaTime*velocidadCarga;
            Debug.Log("Carga al: " + cargaAtaque);
            radioAtaque += Time.deltaTime*2;
            fuerzaEmpuje = cargaAtaque * multEmpuje;
        }
        
        animator.SetBool("CargandoAtk",cargandoAtaque);
        animatorAtks.SetBool("cargandoAtk",cargandoAtaque);
    }

    private void Golpe(float cant){
        Collider2D[] objetos = Physics2D.OverlapCircleAll(puntoAtaque.position,radioAtaque);
        foreach (Collider2D colisionador in objetos){
            if (colisionador.CompareTag("Player2")){
                vida+=(int)cant;
                colisionador.transform.GetComponent<SistemaCombateP2>().TomarDaño(cant);
                colisionador.transform.GetComponent<Rigidbody2D>().AddForce(direccionEmpuje*fuerzaEmpuje,ForceMode2D.Impulse);
            }
        }
    }

    private void FixedUpdate() { //tomar en todo momento la distancia entre los jugadores, se hace un vector para la direccion del empuje
        direccionEmpuje = new Vector2(otroJugador.position.x - this.transform.position.x, otroJugador.position.y - this.transform.position.y);
    }

    public void TomarDaño(float damage){
        vida-=damage;
        if (cargandoAtaque){
            cargandoAtaque=false;
        }
        if (vida<0){gameObject.SetActive(false);}
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(puntoAtaque.position,radioAtaque);
    }
}
