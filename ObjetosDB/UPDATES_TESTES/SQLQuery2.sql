/****** Script for SelectTopNRows command from SSMS  ******/
UPDATE [CTRL_ORCAMENTO_HMG].[dbo].[USUARIO]
SET [BLOQUEADO] = 0,
[VERIFICADO] = 1,
[CLAIM] = 'ADMIN'
   
  
  WHERE [ID] = 1