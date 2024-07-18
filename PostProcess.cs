using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;


public class PostProcess : MonoBehaviour
{
    
    [SerializeField] Material Post;
    [SerializeField] float a;
    [SerializeField] float b;
    [SerializeField] int test;
    [SerializeField] int detailp = 100;
    [SerializeField] int detailp2 = 100;
    [SerializeField] float blurp = 2;
    [SerializeField] float blurp2 = 2;

    [SerializeField] private Texture[] nn;
    private int noise = 0;
 


    private void Start()
    {
        this.GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;
      
   
    }

   
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (Post != null)
        {
//           
          Post.SetFloat("offsetx", Random.value);
          Post.SetFloat("offsety", Random.value);
          Post.SetTexture("_Col", nn[(noise++)%nn.Length]);
            Post.SetFloat("a", a);
            Post.SetFloat("b", b);
          Graphics.Blit(source, destination,Post);
          
        }
        else
        {
            Graphics.Blit(source, destination);
        }

    }
}