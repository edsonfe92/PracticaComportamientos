using System;
using System.Collections.Generic;
using UnityEngine;
using BehaviourAPI.Core;
using BehaviourAPI.Core.Actions;
using BehaviourAPI.Core.Perceptions;
using BehaviourAPI.UnityToolkit;
using BehaviourAPI.UtilitySystems;

public class DueloRunner : BehaviourRunner
{
	
	
	protected override BehaviourGraph CreateGraph()
	{
		UtilitySystem Duelo = new UtilitySystem(1.3f);
		
		VariableFactor EnMovimiento = Duelo.CreateVariable(MovementFactor, 0f, 1f);
		
		VariableFactor Posicion = Duelo.CreateVariable(PositionFactor, 0f, 1f);
		
		VariableFactor NDR = Duelo.CreateVariable(AmmoFactor, 0f, 6f);
		
		SigmoidCurveFactor Municion = Duelo.CreateCurve<SigmoidCurveFactor>(NDR);
		
		FunctionalAction Recargar_action = new FunctionalAction();
		Recargar_action.onStarted = MissingBullets;
		Recargar_action.onUpdated = Reloading;
		Recargar_action.onStopped = EndReload;
		UtilityAction Recargar = Duelo.CreateAction(Municion, Recargar_action);
		
		LinearCurveFactor Expuesto = Duelo.CreateCurve<LinearCurveFactor>(EnMovimiento);
		
		LinearCurveFactor Cubierto = Duelo.CreateCurve<LinearCurveFactor>(Posicion);
		
		WeightedFusionFactor Apuntar = Duelo.CreateFusion<WeightedFusionFactor>(Expuesto, Cubierto);
		
		FunctionalAction Disparar_action = new FunctionalAction();
		Disparar_action.onStarted = EnemyDetected;
		Disparar_action.onUpdated = EnemyShot;
		Disparar_action.onStopped = EnemyKilled;
		Disparar_action.onPaused = Reload;
		Disparar_action.onUnpaused = EndReload;
		UtilityAction Disparar = Duelo.CreateAction(Apuntar, Disparar_action);
		
		LinearCurveFactor ATiro = Duelo.CreateCurve<LinearCurveFactor>(Posicion);
		
		FunctionalAction Cubrirse_action = new FunctionalAction();
		Cubrirse_action.onStarted = CoverDetected;
		Cubrirse_action.onUpdated = PauseShooting;
		Cubrirse_action.onStopped = DetectedByEnemy;
		UtilityAction Cubrirse = Duelo.CreateAction(ATiro, Cubrirse_action);
		
		return Duelo;
	}
	
	private float MovementFactor()
	{
		throw new System.NotImplementedException();
	}
	
	private float PositionFactor()
	{
		throw new System.NotImplementedException();
	}
	
	private float AmmoFactor()
	{
		throw new System.NotImplementedException();
	}
	
	private void MissingBullets()
	{
		throw new System.NotImplementedException();
	}
	
	private Status Reloading()
	{
		throw new System.NotImplementedException();
	}
    private void Reload()
    {
        throw new System.NotImplementedException();
    }

    private void EndReload()
	{
		throw new System.NotImplementedException();
	}
	
	private void EnemyDetected()
	{
		throw new System.NotImplementedException();
	}
	
	private Status EnemyShot()
	{
		throw new System.NotImplementedException();
	}
	
	private void EnemyKilled()
	{
		throw new System.NotImplementedException();
	}
	
	private void CoverDetected()
	{
		throw new System.NotImplementedException();
	}
	
	private Status PauseShooting()
	{
		throw new System.NotImplementedException();
	}
	
	private void DetectedByEnemy()
	{
		throw new System.NotImplementedException();
	}
}
