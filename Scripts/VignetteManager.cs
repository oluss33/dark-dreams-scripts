using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

namespace Scripts {
    public class VignetteManager : MonoBehaviour
    {
        public static VignetteManager instance { get; private set; }
        Player.Player player = Player.Player.main;

        [Header("Vignette")]
        public Color normalColor;
        public Color firstColor;
        public Color secondColor;
        public Color thirdColor;
        public Vignette vignette;
        public Volume vol;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            vol.profile.TryGet(out vignette);
        }
        public void SetVignette()
        {
           /* if (player.curHeadHp > 80 || player.curBodyHp > 80 || player.curLegsHp > 90) vignette.color.value = normalColor;

            if (player.curHeadHp <= 80 && player.curHeadHp > 70 || player.curBodyHp <= 80 && player.curBodyHp > 70 || player.curLegsHp <= 80 && player.curLegsHp > 70)
                vignette.color.value = firstColor;

            if (player.curHeadHp <= 70 && player.curHeadHp > 50 || player.curBodyHp <= 70 && player.curBodyHp > 50 || player.curLegsHp <= 70 && player.curLegsHp > 50)
                vignette.color.value = secondColor;

            if (player.curHeadHp <= 50 && player.curHeadHp > 30 || player.curBodyHp <= 50 && player.curBodyHp > 30 || player.curLegsHp <= 50 && player.curLegsHp > 30)
                vignette.color.value = thirdColor;*/
        }
    }
}