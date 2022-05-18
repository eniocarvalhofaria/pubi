
insert into reports.SFOwner

 select 
 	cast(id as char(15))
 ,	name
 from salesforce.sfUSer
 where cast(id as char(15)) not in (select id from reports.SFOwner)
 union all 
 
 select 
 	cast(id as char(15))
 ,	name
 from salesforce.ctcadastro_de_funcion_rios
 where cast(id as char(15)) not in (select id from reports.SFOwner)
;
update reports.SFOwner
 SET SalesPersonId = 
(
	SELECT  SalesPersonId2 FROM 
	(	
	select 
		min(SalesPersonId) SalesPersonId2
	,	salespersonname
	from reports.salesperson
	group by 2
	) x
     WHERE x.salespersonname = reports.SFOwner.name
     
     
)
where salespersonid is null
;
     
COMMIT
;

select 
	cast(c.Name as int) ContractId
,	c.Id SalesForceContractId
,	CreatedById CreatedUserId
,	Description
,	Destaque_MGR Description_MGR
,	Account AccountId
,	cast(CustomerSignedDate as date) CustomerSignedDate
,	cast(dateadd(hour,-3,c.CreatedDate) as date) CreatedDate
,	cast(Data_do_envio_para_aprova_o as date) SubmissionDate
,	Aprovado_pelo_Coord_Vendas_em ApprovalDateSalesCoordinator
,	Aprovado_pelo_Coord_Regional_em ApprovalDateManager
,	Aprovado_pelo_VT_em ApprovalDateTravel
,	Aprovado_pelo_Coord_Publica_o_em ApprovalDatePublicationCoordinator
,	Coordenador_de_Vendas SalesCoordinatorId
,	CV_Regional ManagerId
,	Coordenador_de_Publica_o PublicationCoordinatorId
,	Nome_da_Oportunidade OpportunityId
,	Fase StageName
,	CV_Hier_rquico SalesCoordinator
,	coalesce(cast(dl.publishing_date as date),cast(Data_de_Publicacao as date)) PublishingDate
,	case
		when ramo_de_atua_o ilike '%gastron%'
		then 3
		when ramo_de_atua_o ilike '%viage%'
		then 1
		when ramo_de_atua_o ilike '%turismo%'
		then 1
		when ramo_de_atua_o ilike '%hote%'
		then 1
		when ramo_de_atua_o ilike '%beleza%'
		then 2
		when ramo_de_atua_o ilike '%Estética%'
		then 2
		when ramo_de_atua_o ilike '%bem estar%'
		then 2
		when ramo_de_atua_o ilike '%Online%'
		then 10
		when ramo_de_atua_o ilike '%Produto%'
		then 10
		when ramo_de_atua_o ilike '%Fotografia%'
		then 10
		when ramo_de_atua_o ilike '%Locais%'
		then 11
		when ramo_de_atua_o ilike '%Automotivo%'
		then 11
		when ramo_de_atua_o ilike '%Crianças%'
		then 11
		when ramo_de_atua_o ilike '%Serviços%'
		then 11
		when ramo_de_atua_o ilike '%cursos%'
		then 11
		when ramo_de_atua_o ilike '%Entretenimento%'
		then 4
		when ramo_de_atua_o ilike '%lazer%'
		then 4
		else 12
	end CategoryId
,	Sub_Ramo_de_Atua_o Subcategory
,	CodPagamentoSAP PaymentCondition
,	Condicao_de_pagamento PaymentConditionName
,	Motivo_de_rejeicao_CP ReasonForRejection
,	cast((Comiss_o_do_peixe_urbano/1000) as numeric(15,2)) OurCommision
,	Comiss_o_do_peixe_urbano OurCommisionSF
,	PreOriginal OriginalPrice
,	Pre_o_Total_com_Desconto CouponPrice
,	Valor_repassado_ao_PU_por_unidade PartPU
,	Valor_repassado_ao_Parceiro_por_unidade PartPartner
,	coalesce(Conta_CNPJ_Faturamento,Conta_Cpf_Faturamento) PaymentDocumentNumber
,	Ofertanositeapospublicacao DealsClub
,	Agendamento_ser_feito SchedulePlataform
,	necess_rio_fazer_reserva ScheduleThis
,	Antecedencia_minima_agendamento LeastScheduling
,	case
		when
		Cupom_com_uso_imediato =true
		and
		case
			when necess_rio_levar_o_cupom_impresso in ('Obrigatório cupom impresso','Obrigat??rio cupom impresso','Sim')
			then 'Obrigatório cupom impresso'
			when necess_rio_levar_o_cupom_impresso in ('Apresentar cupom no aplicativo oficial do Peixe Urbano ou impresso (Recomendado)','Não precisa de cupom (com??rcio online com validação no site Peixe Urbano)','N??o precisa de cupom (comércio online com validação no site Peixe Urbano)','Basta informar o c??digo do cupom da forma que preferir','Não','Basta informar o código do cupom da forma que preferir','Não precisa de cupom (comércio online com validação no site Peixe Urbano)','Usar cupom no site do Parceiro')
			then 'Não precisa de cupom impresso'
			when necess_rio_levar_o_cupom_impresso is null
			then 'Obrigatório cupom impresso'
			else necess_rio_levar_o_cupom_impresso
		end = 'Obrigatório cupom impresso'
		then 0
		when Cupom_com_uso_imediato = true then 1
		else 0
	end UseNow
,	cast(LastModifiedDate as date) LastModifiedDate
,	Fracionamento Subdivision
,	Cidade_da_Oferta DealCity
,	Tipo_Site TypeSite
,	coalesce(dl.category,Categoria_Site) CategorySite
,	coalesce(dl.phoenix_category,Subcategoria_Site) SubcategorySite
,	Oferta_Especial_de_Marketing MarketingSpecialDeal
,	case
		when Expectativa_Qualidade = 'Inferior a Low' then '1- Inferior a Low'
		when Expectativa_Qualidade = 'Low' then '2- Low'
		when Expectativa_Qualidade = 'Medium' then '3- Medium'
		when Expectativa_Qualidade = 'High' then '4- High'
		when Expectativa_Qualidade = 'High High' then '5- High High'
		else null
	end Expectation
,	case
		when Oportunidade_Expectativa_de_Comiss_o > 0 and Oportunidade_Expectativa_do_Ticket_M_dio > 0 and Oportunidade_Expectativa_Vendas_Cupons > 0
		then (Oportunidade_Expectativa_do_Ticket_M_dio * Oportunidade_Expectativa_Vendas_Cupons) * Oportunidade_Expectativa_de_Comiss_o
		else 0
	end * 0.0055 CommercialExpectationValue
,	case
		when (case
		when Oportunidade_Expectativa_de_Comiss_o > 0 and Oportunidade_Expectativa_do_Ticket_M_dio > 0 and Oportunidade_Expectativa_Vendas_Cupons > 0
		then (Oportunidade_Expectativa_do_Ticket_M_dio * Oportunidade_Expectativa_Vendas_Cupons) * Oportunidade_Expectativa_de_Comiss_o
		else 0
	end * 0.0055) = 0 then '2- Low'
		when (case
		when Oportunidade_Expectativa_de_Comiss_o > 0 and Oportunidade_Expectativa_do_Ticket_M_dio > 0 and Oportunidade_Expectativa_Vendas_Cupons > 0
		then (Oportunidade_Expectativa_do_Ticket_M_dio * Oportunidade_Expectativa_Vendas_Cupons) * Oportunidade_Expectativa_de_Comiss_o
		else 0
	end * 0.0055) < 600 then '1- Inferior a Low'
		when (case
		when Oportunidade_Expectativa_de_Comiss_o > 0 and Oportunidade_Expectativa_do_Ticket_M_dio > 0 and Oportunidade_Expectativa_Vendas_Cupons > 0
		then (Oportunidade_Expectativa_do_Ticket_M_dio * Oportunidade_Expectativa_Vendas_Cupons) * Oportunidade_Expectativa_de_Comiss_o
		else 0
	end * 0.0055) < 3000 then '2- Low'
		when (case
		when Oportunidade_Expectativa_de_Comiss_o > 0 and Oportunidade_Expectativa_do_Ticket_M_dio > 0 and Oportunidade_Expectativa_Vendas_Cupons > 0
		then (Oportunidade_Expectativa_do_Ticket_M_dio * Oportunidade_Expectativa_Vendas_Cupons) * Oportunidade_Expectativa_de_Comiss_o
		else 0
	end * 0.0055) < 6000 then '3- Medium'
		when (case
		when Oportunidade_Expectativa_de_Comiss_o > 0 and Oportunidade_Expectativa_do_Ticket_M_dio > 0 and Oportunidade_Expectativa_Vendas_Cupons > 0
		then (Oportunidade_Expectativa_do_Ticket_M_dio * Oportunidade_Expectativa_Vendas_Cupons) * Oportunidade_Expectativa_de_Comiss_o
		else 0
	end * 0.0055) < 15000 then '4- High'
		when (case
		when Oportunidade_Expectativa_de_Comiss_o > 0 and Oportunidade_Expectativa_do_Ticket_M_dio > 0 and Oportunidade_Expectativa_Vendas_Cupons > 0
		then (Oportunidade_Expectativa_do_Ticket_M_dio * Oportunidade_Expectativa_Vendas_Cupons) * Oportunidade_Expectativa_de_Comiss_o
		else 0
	end * 0.0055) >= 15000 then '5- High High'
	end CommercialExpectation
,	case
			when necess_rio_levar_o_cupom_impresso in ('Obrigatório cupom impresso','Obrigat??rio cupom impresso','Sim')
			then 'Obrigatório cupom impresso'
			when necess_rio_levar_o_cupom_impresso in ('Apresentar cupom no aplicativo oficial do Peixe Urbano ou impresso (Recomendado)','Não precisa de cupom (com??rcio online com validação no site Peixe Urbano)','N??o precisa de cupom (comércio online com validação no site Peixe Urbano)','Basta informar o c??digo do cupom da forma que preferir','Não','Basta informar o código do cupom da forma que preferir','Não precisa de cupom (comércio online com validação no site Peixe Urbano)','Usar cupom no site do Parceiro')
			then 'Não precisa de cupom impresso'
			when necess_rio_levar_o_cupom_impresso is null
			then 'Obrigatório cupom impresso'
			else necess_rio_levar_o_cupom_impresso
	end UtilizationType
,	Prazo_para_agendamento_do_cupom CouponScheduleDeadline
,   Converteu_Use_Agora ConvertedUseNow
,	coalesce(o2.name,o3.name) OwnerName
,	n_mero_m_ximo_de_cupons MaxCouponsQty
into #SFContract
from  salesforce.ctContratoPU c
left join 
(
	select
		cast(contract_number as int) contract_number
--	contract_number
	,	min(case when trim(category) = '' then null else trim(category) end ) category
	,	min(case when trim(phoenix_category) = '' then null else trim(phoenix_category) end ) phoenix_category
	,	min(publishing_date) publishing_date

	from ods.mng_deals_latest
	where trim(contract_number) <> ''
	group by 1
	order by 1
	
) dl
on dl.contract_number = cast(c.Name as int)
left join reports.SFOwner o2
on cast(c.propriet_rio_compartilhado as char(15)) = o2.id
left join reports.SFOwner o3
on cast(c.owner2 as char(15)) = o3.id
;

delete from reports.SFcontract
;
insert into reports.SFcontract
select * from #SFContract
;
COMMIT
;



select 
	c_digo_opcao_n_merico BuyingOptionId
,	id SalesforceBuyingOptionId
,	cast(substring(nome_da_oferta_texto ,4,10) as int) OfferId
,	contrato_num_rico ContractId
,	case 
		when not(propriet_rio_compartilhado is null or trim(propriet_rio_compartilhado) = '')
		then cast(id_propriet_rio_compartilhado as char(15))
		when not(nome_propriet_rio_final is null or trim(nome_propriet_rio_final) = '')
		then cast(id_propriet_rio_final as char(15))
		else null
	end SalesforceOwnerId
,	createddate
into #SFBuyingOption
from salesforce.ctop_es_de_Compra
;
delete from reports.SFBuyingOption
;
insert into reports.SFBuyingOption
select * from #SFBuyingOption
;
COMMIT
;

select 
	c_digo_oferta_num_rico OfferId
,	id SalesforceOfferId
,	case 
		when not(propriet_rio_compartilhado is null or trim(propriet_rio_compartilhado) = '')
		then cast(propriet_rio_compartilhado  as char(15))
		when not(of_propriet_rio_da_oferta is null or trim(of_propriet_rio_da_oferta) = '')
		then cast(of_propriet_rio_da_oferta as char(15))
		else null
	end SalesforceOwnerId
,	case
		when conta_ramo_de_atua_o ilike '%gastron%'
		then 3
		when conta_ramo_de_atua_o ilike '%viage%'
		then 1
		when conta_ramo_de_atua_o ilike '%turismo%'
		then 1
		when conta_ramo_de_atua_o ilike '%hote%'
		then 1
		when conta_ramo_de_atua_o ilike '%beleza%'
		then 2
		when conta_ramo_de_atua_o ilike '%Estética%'
		then 2
		when conta_ramo_de_atua_o ilike '%bem estar%'
		then 2
		when conta_ramo_de_atua_o ilike '%Online%'
		then 10
		when conta_ramo_de_atua_o ilike '%Produto%'
		then 10
		when conta_ramo_de_atua_o ilike '%Fotografia%'
		then 10
		when conta_ramo_de_atua_o ilike '%Locais%'
		then 11
		when conta_ramo_de_atua_o ilike '%Automotivo%'
		then 11
		when conta_ramo_de_atua_o ilike '%Crianças%'
		then 11
		when conta_ramo_de_atua_o ilike '%Serviços%'
		then 11
		when conta_ramo_de_atua_o ilike '%cursos%'
		then 11
		when conta_ramo_de_atua_o ilike '%Entretenimento%'
		then 4
		when conta_ramo_de_atua_o ilike '%lazer%'
		then 4
		else 12
	end CategoryId
,	coalesce(UseNow,0) UseNow
,	createddate
into #SFOffer
from salesforce.ctofertas
left join
(
	select
		OfferId
	,	case 
			when cupom_impresso ='Apresentar cupom no aplicativo oficial do Peixe Urbano ou impresso (Recomendado)' and agendamento = '' then 1
			when cupom_impresso ='Apresentar cupom no aplicativo oficial do Peixe Urbano ou impresso (Recomendado)' and agendamento ='Não precisa agendar' then 1
			when cupom_impresso ='Apresentar cupom no aplicativo oficial do Peixe Urbano ou impresso (Recomendado)' and agendamento ='Site do Parceiro' then 1
		 	when cupom_impresso ='Basta informar o código do cupom da forma que preferir' and agendamento ='' then 1
		 	when cupom_impresso ='Basta informar o código do cupom da forma que preferir' and agendamento ='Não precisa agendar' then 1
		 	when cupom_impresso ='Basta informar o código do cupom da forma que preferir' and agendamento ='Site do Parceiro' then 1
		 	when cupom_impresso ='Apresentar Cupom no App ou Impresso (Recomendado)' and agendamento ='' then 1
		 	when cupom_impresso ='Apresentar Cupom no App ou Impresso (Recomendado)' and agendamento ='Não precisa agendar' then 1
		 	when cupom_impresso ='Apresentar Cupom no App ou Impresso (Recomendado)'and agendamento ='Site do Parceiro' then 1
		 	else 0
		end UseNow
	from
	(
		SELECT c_digo_oferta_num_rico OfferId
		,	CASE 
				WHEN cto.of_necess_rio_levar_o_cupom_impresso is not null then cto.of_necess_rio_levar_o_cupom_impresso 
				ELSE ccp.necess_rio_levar_o_cupom_impresso 
			END as cupom_Impresso 
		,	CASE 
				WHEN cto.of_m_todo_de_agendamento is not null then cto.of_m_todo_de_agendamento 
				ELSE ccp.agendamento_ser_feito 
			END as Agendamento
		FROM salesforce.ctofertas cto
		left join salesforce.ctcontratopu ccp 					
		on cto.contrato_pu = ccp.id
	) x
	
) u
on u.OfferId = c_digo_oferta_num_rico
;
delete from reports.SFOffer
;
insert into reports.SFOffer
select * from #SFOffer
;
COMMIT
;

select
	id salesforceaccountid
,	matriz_ou_filial HeadQuartersOrBranch
,	case when rede_franquia = 'Sim' then 1 else 0 end IsChainOrFranchise
,	null EconomicGroupName
,	lista_de_acompanhamento WatchList
,	case when parceiro_groupon = 'Sim' then 1 else 0 end IsGrouponPartner
,	propriet_rio_grow_team SalesforceGrowTeamOwnerId
,	case 
		when not(propriet_rio_compartilhado is null or trim(propriet_rio_compartilhado) = '')
		then cast(propriet_rio_compartilhado as char(15))
		when not(proprietario_anterior is null or trim(proprietario_anterior) = '')
		then cast(proprietario_anterior as char(15))
		else null
	end SalesforceOwnerId
,	case 
		when Atendimento_Espelhado like '%/%' 
		then cast(substring(Atendimento_Espelhado,4,2) as int)
		else 0 
	end sharedServicePercent
,	cast(Respons_vel_Atendimento_Espelhado as char(15)) sharedServiceOwner
,	case when Apura_o_pelo_Contrato = 'true' then 1 else 0 end IsCalculatedByContract
into #SFAccount
from salesforce.cfAccount
;
delete from reports.SFAccount
;
insert into reports.SFAccount
select * from #SFAccount
;
COMMIT
;


select 
	s.id SalesForceOpportunityId
,	accountid SalesForceAccountId
,	cast(propriet_rio_compartilhado as char(15)) SalesForceOwnerId
,	createddate
,	stagename
into #SFOpportunity
from salesforce.cfopportunity c
inner join salesforce.sfopportunity s
on c.id = s.id

;
delete from reports.SFOpportunity
;
insert into reports.SFOpportunity
select * from #SFOpportunity
;
COMMIT
;
