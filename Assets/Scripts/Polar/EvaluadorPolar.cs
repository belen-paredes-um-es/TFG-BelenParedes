using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text.RegularExpressions;

public class EvaluadorPolar : MonoBehaviour
{
    [HideInInspector]
    public float tam = 1f;
    [HideInInspector]
    public int numVueltas=12;
    [HideInInspector]
    public int numPuntos=101;

    [HideInInspector]
    public string texto="", textoOf, t, c1,c2;
    [HideInInspector]
    public int var1,var2;
    string[] claves;
    string miFuncion;

    int n;
    LineRenderer curva;
    MallasCerradas mallasCerradas;
    float ang,T,r;
    Vector3 P;
    

    public float evaluar(string texto,string miT=""){
        claves=new string[6];
        claves[0]="sin("; 
        claves[1]="cos("; 
        claves[2]="abs("; 
        claves[3]="pow("; 
        claves[4]="exp(";
        claves[5]="log(";  
        texto=texto.Replace(" ","");

        if(miT==""){
            texto=texto.Replace("t",t);    
        }
        else{
            texto=texto.Replace("t",miT);
        }

        c1= var1+"";
        c2= var2+"";

        texto=texto.Replace("t",t);
        texto=texto.Replace("a",c1);
        texto=texto.Replace("b",c2);
        texto=texto.Replace("PI","3,14159265");
        string st1, st2, st3;
        bool funcion=false;
        
        foreach(string clave in claves){

            if(texto.IndexOf(clave)>-1){
                miFuncion=clave;
                funcion=true;
                
            }
        }

        if(! funcion){
            if(! texto.Contains("(") & !texto.Contains(")")){
                if(texto.Contains("+")){
                    int i=texto.IndexOf("+");
                    st1=texto.Substring(0, i);
                    st2=texto.Substring(i + 1);
                    return(evaluar(st1)+evaluar(st2));
                }
                else{
                    int ii=texto.IndexOf("-");
                    bool vale=true;
                    if(ii==0){
                        vale=false;
                    }
                    else{
                        if(ii>0){
                            if(texto[ii-1]=='E'){
                                vale=false;
                            }
                        }
                         
                    }
                    if(texto.Contains("-") & vale){
                        int i=texto.IndexOf("-");
                        st1=texto.Substring(0, i);
                        st2=texto.Substring(i + 1);
                        return(evaluar(st1)-evaluar(st2));

                    }
                    else{
                        if(texto.Contains("*")){
                            int i=texto.IndexOf("*");
                            st1=texto.Substring(0, i);
                            st2=texto.Substring(i + 1);
                            return(evaluar(st1)*evaluar(st2));
                        }
                        else{
                            if(texto.Contains("/")){
                                int i=texto.IndexOf("/");
                                st1=texto.Substring(0, i);
                                st2=texto.Substring(i + 1);
                                return(evaluar(st1)/evaluar(st2));
                            }
                            else{
                                float a=float.Parse(texto);
                                return(a); 
                            }
                        }
                    }
                }
            }
            else{
                int i0=texto.IndexOf("(");
                int i1=cerrar(texto);
                if(i1==-1){
                    return(-1);
                }
                else{
                    if(i0==0){
                        st1="";
                    }
                    else{
                        st1=texto.Substring(0,i0);
                        
                    }
                    if(i1==texto.Length-1){
                        st2="";
                    }
                    else{
                        st2=texto.Substring(i1+1);
                    }
                    if(i1==i0+1){
                        st3="";
                    }
                    else{
                        st3=texto.Substring(i0+1,i1-i0-1);
                    }

                    if(! (st3=="")){
                        st3=evaluar(st3).ToString();
                        st1=st1+st3+st2;
                        return(evaluar(st1));
                    }
                    return(-1);
                }
            }
            
        }
        else{
            if(miFuncion=="sin("){
                int i0=texto.IndexOf(miFuncion);
                int i1=cerrarFuncion(texto,miFuncion);
                int i00=i0+miFuncion.Length;
                if(i1>-1){
                    string argumento=texto.Substring(i00,i1-i00);
                    float arg=evaluar(argumento);
                    float val=Mathf.Sin(arg);
                    string cadena=texto.Substring(i0,i1-i0+1);
                    
                    texto=texto.Replace(cadena,val.ToString());
                    return(evaluar(texto));

                }
            }
            if(miFuncion=="cos("){
                int i0=texto.IndexOf(miFuncion);
                int i1=cerrarFuncion(texto,miFuncion);
                int i00=i0+miFuncion.Length;
                if(i1>-1){
                    string argumento=texto.Substring(i00,i1-i00);
                    float arg=evaluar(argumento);
                    float val=Mathf.Cos(arg);
                    string cadena=texto.Substring(i0,i1-i0+1);
                    
                    texto=texto.Replace(cadena,val.ToString());
                    return(evaluar(texto));

                }
            }
            if(miFuncion=="abs("){
                int i0=texto.IndexOf(miFuncion);
                int i1=cerrarFuncion(texto,miFuncion);
                int i00=i0+miFuncion.Length;
                if(i1>-1){
                    string argumento=texto.Substring(i00,i1-i00);
                    float arg=evaluar(argumento);
                    float val=Mathf.Abs(arg);
                    string cadena=texto.Substring(i0,i1-i0+1);
                    
                    texto=texto.Replace(cadena,val.ToString());
                    return(evaluar(texto));

                }
            }
            if(miFuncion=="log("){
                int i0=texto.IndexOf(miFuncion);
                int i1=cerrarFuncion(texto,miFuncion);
                int i00=i0+miFuncion.Length;
                if(i1>-1){
                    string argumento=texto.Substring(i00,i1-i00);
                    float arg=evaluar(argumento);
                    float val=Mathf.Log(arg);
                    string cadena=texto.Substring(i0,i1-i0+1);
                    
                    texto=texto.Replace(cadena,val.ToString());
                    return(evaluar(texto));

                }
            }
            if(miFuncion=="exp("){
                int i0=texto.IndexOf(miFuncion);
                int i1=cerrarFuncion(texto,miFuncion);
                int i00=i0+miFuncion.Length;
                if(i1>-1){
                    string argumento=texto.Substring(i00,i1-i00);
                    float arg=evaluar(argumento);
                    float val=Mathf.Exp(arg);
                    string cadena=texto.Substring(i0,i1-i0+1);
                    texto=texto.Replace(cadena,val.ToString());
                    return(evaluar(texto));

                }
            }

            if(miFuncion=="pow("){
                int i0=texto.IndexOf(miFuncion);
                int i1=cerrarFuncion(texto,miFuncion);
                int i00=i0+miFuncion.Length;
                if(i1>-1){
                    string argumento=texto.Substring(i00,i1-i00);
                    string[] argumentos=argumento.Split(',');
                    float arg1=evaluar(argumentos[0]);
                    float arg2=evaluar(argumentos[1]);
                    float val=Mathf.Pow(arg1,arg2);
                    string cadena=texto.Substring(i0,i1-i0+1);
                    texto=texto.Replace(cadena,val.ToString());
                    return(evaluar(texto));

                }
            }
            return(-2);
        }
    }



    public int contar(string texto, char subtexto, int i0=-1, int i1=-1){
        int cont=0;
        if(i0==-1){
            i0=0;
        }
        if(i1==-1){
            i1=texto.Length;
        }
        for(int i=i0; i<i1;i++){
            if(texto[i]==subtexto){
                cont+=1;
            }
        }
        return(cont);
    }

    public int cerrar(string texto){
        if(texto.IndexOf('(')==-1){
            return(-1);
        }
        else{
            int i0=texto.IndexOf('(');
            for(int i1=i0+1;i1<texto.Length;i1++){
                if(texto[i1]==')'){
                    string subtexto=texto.Substring(i0+1,i1-i0-1);
                    int j0=contar(subtexto,'(');
                    int j1=contar(subtexto,')');
                    if(j0==j1){
                        return(i1);
                    }
                }
            }
            return(-1);
        }
    }

    public int cerrarFuncion(string texto, string miFuncion){
        if(texto.IndexOf(miFuncion)==-1){
            return(-1);
        }
        else{
            int i0=texto.IndexOf(miFuncion)+miFuncion.Length;
            for(int i1=i0+1;i1<texto.Length;i1++){
                if(texto[i1]==')'){
                    string subtexto=texto.Substring(i0+1,i1-i0-1);
                    int j0=contar(subtexto,'(');
                    int j1=contar(subtexto,')');
                    if(j0==j1){
                        return(i1);
                    }
                }
            }
            return(-1);
        }
    }

    public void DibujarMalla(){
        mallasCerradas = gameObject.GetComponent<MallasCerradas>();
        mallasCerradas.DibujarMalla();
    }

    public void nuevaEcuacion(){
        texto=textoOf;
    }

    public void eliminarCurva(){
        curva = GetComponent<LineRenderer>();
        curva.positionCount = 1;
    }

    public void dibujarCurva(){
        n = numPuntos * numVueltas;
        curva = GetComponent<LineRenderer>();
        curva.positionCount = n;

        for(int i = 0; i < n; i++){
            ang = 2 * Mathf.PI *numVueltas;
            T = 1f* i / n * ang;
            r = evaluar(texto, T.ToString());
            P = r * new Vector3(Mathf.Cos(T),Mathf.Sin(T),0);
            curva.SetPosition(i,P);
        }
        DibujarMalla();
    }
}
