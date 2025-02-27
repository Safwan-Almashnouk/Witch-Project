     /// using System.Collections;
     /// using System.Collections.Generic;
     /// using UnityEngine;
     ///
     /// public class MeleeManager : MonoBehaviour
     /// {
     ///     private MeleeContext context;
     ///     private SpearStrategy spear;
     ///     private GreatSwordStrategy greatSword;
     ///     void Start()
     ///     {
     ///         context = GetComponent<MeleeContext>();
     ///         spear = GetComponent<SpearStrategy>();
     ///         greatSword = GetComponent<GreatSwordStrategy>();
     ///         context.SetAttackStrategy(greatSword);
     ///     }
     ///
     ///     // Update is called once per frame
     ///     void Update()
     ///     {
     /// 
     ///     }
     /// }
     ///