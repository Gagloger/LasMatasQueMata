using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuntajeJugador : MonoBehaviour
{
    [SerializeField] private float puntaje;
    [SerializeField] private float cantidadPuntaje;
    private bool ganandoPuntos;

    private Animator animator;
    [SerializeField] private GameObject efectoNutriendose;

    private void Start() {
        puntaje=0f;
        ganandoPuntos=false;
        animator = GetComponent<Animator>();
    }

    public void CambioDeEstado()
    {
        ganandoPuntos= !ganandoPuntos;
    }

    private void Update(){
        if (ganandoPuntos){
            puntaje += cantidadPuntaje*Time.deltaTime;
            efectoNutriendose.SetActive(true);
        } else {efectoNutriendose.SetActive(false);}
        animator.SetBool("Nutriendose",ganandoPuntos);
    }

    public void Muerte(){
        if (puntaje > 0f){
            puntaje-=10f;
        }
    }
    public void Kill(){
        if (puntaje > 0f){
            puntaje+=10f;
        }
    }
}
