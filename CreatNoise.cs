using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Experimental.Rendering;
using UnityEditor;
using UnityEngine.Windows;
using Input = UnityEngine.Input;
using Random = UnityEngine.Random;


public class CreatNoise : MonoBehaviour
{
    
    [SerializeField] private ComputeShader c;
    [SerializeField]private RenderTexture r, r1,r2, rsult;
    
    [SerializeField] private Material mat;

    [SerializeField] float persistance = 0.5f;
    [SerializeField] float amp = 1;
    [SerializeField] float freq = 8;
    [SerializeField] float freqchange = 2;
    [SerializeField] float seed = 2;
    [SerializeField] int octaves = 4;
    [SerializeField] private Vector2 offset = Vector2.zero;

    private const string filePath = "ENTER HERE";

    
    
    // Start is called before the first frame update
    void Start()
    {
       
  
      r = RenderTexture.GetTemporary(1920,1080,0);
      r1 = RenderTexture.GetTemporary(1920,1080,0);
      r2 = RenderTexture.GetTemporary(1920,1080,0);
       r.enableRandomWrite = true;
       r1.enableRandomWrite = true;
       r2.enableRandomWrite = true;

       r.Create();
       r1.Create();
       r2.Create();
        c.SetTexture(0,"Result",r1);
        c.SetFloat("persistance", persistance);
        c.SetFloat("amp", amp);
        c.SetFloat("freq", freq);
        c.SetFloat("freqchange", freqchange);
        c.SetFloat("seed", seed);
        c.SetInt("octaves", octaves);
        c.SetVector("offset", offset);
  
        c.Dispatch(0, 1920/8,1080/8,1);

    }
    Texture2D ToTexture2D(RenderTexture rTex)
    {
        Texture2D tex = new Texture2D(1920, 1080);
        RenderTexture.active = rTex;
        tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        tex.Apply();
        return tex;
    }
    // Update is called once per frame
    void Update()
    {
   
     RenderTexture.ReleaseTemporary(r);
     RenderTexture.ReleaseTemporary(r1);
     RenderTexture.ReleaseTemporary(r2);
     RenderTexture.ReleaseTemporary(rsult);

   r = RenderTexture.GetTemporary(1920,1080,0);
   r1 = RenderTexture.GetTemporary(1920,1080,0);
   r2 = RenderTexture.GetTemporary(1920,1080,0);
   rsult = RenderTexture.GetTemporary(1920,1080,0);
   r.enableRandomWrite = true;
   r1.enableRandomWrite = true;
   r2.enableRandomWrite = true;
   rsult.enableRandomWrite = true;
    
     r.Create();
     r1.Create();
     r2.Create();
     rsult.Create();
     c.SetTexture(0,"Result",r);
     c.SetFloat("persistance", persistance);
     c.SetFloat("amp", 0.3f*amp);
     c.SetFloat("freq", freq);
     c.SetFloat("freqchange", freqchange);
     c.SetFloat("seed", Random.Range(0,1000));
     c.SetInt("octaves", octaves);
     c.SetVector("offset", new Vector2(770.72f, -2100f));
     c.Dispatch(0, 1920/8,1080/8,1);

  
     c.SetTexture(0,"Result",r1);
     c.SetFloat("persistance", persistance);
     c.SetFloat("amp", 0.15f*amp);
     c.SetFloat("freq", freq);
     c.SetFloat("freqchange", freqchange);
     c.SetFloat("seed", Random.Range(0,1000));
     c.SetInt("octaves", octaves);
     c.SetVector("offset", new Vector2(770.72f, -6));
     c.Dispatch(0, 1920/8,1080/8,1);

 
     c.SetTexture(0,"Result",r2);
     c.SetFloat("persistance", persistance);
     c.SetFloat("amp", amp*0.5f);
     c.SetFloat("freq", freq);
     c.SetFloat("freqchange", freqchange);
     c.SetFloat("seed", Random.Range(0,1000));
     c.SetInt("octaves", octaves);
     c.SetVector("offset", new Vector2(45.72f, -2100f));
     c.Dispatch(0, 1920/8,1080/8,1);

      c.SetTexture(1,"Result",rsult);
      c.SetTexture(1,"r",r);
      c.SetTexture(1,"g",r1);
      c.SetTexture(1,"b",r2);
      c.Dispatch(1, 1920/8,1080/8,1);
     
        mat.SetTexture("_MainTex", rsult);
           if (Input.GetKeyDown(KeyCode.Space))
           {
               Texture2D a = ToTexture2D(rsult);
               File.WriteAllBytes(filePath, a.EncodeToPNG());
                print("ya");
           }
        
        
    }
}
