
drop table if exists #categoryNames
;
select distinct name 
into #categoryNames
from 
(
select distinct tipo_site_oferta name  from salesforce.ctop_es_de_compra
union
select distinct categoria_site_oferta name  from salesforce.ctop_es_de_compra
union
select distinct Subcategoria_Site_Oferta name  from salesforce.ctop_es_de_compra
union 
select distinct Sub_Subcategoria_Site_Oferta name   from salesforce.ctop_es_de_compra
)

;

drop table if exists #categoryCorrection
;
SELECT
	name
,	name_erro
into #categoryCorrection
from

(
select distinct 
	replace(name,'&','e') name
,	replace(replace(replace(replace(replace(replace(replace(replace(replace(replace(replace(replace(lower(name),'ó',''),'õ',''),'ô',''),'á',''),'ã',''),'â',''),'é',''),'ê',''),'í',''),'ú',''),'ç',''),'&','e') name_Sup
from  #categoryNames
where lower(name) like '%ó%'
or lower(name) like '%õ%'
or lower(name) like '%ô%'
or lower(name) like '%â%'
or lower(name) like '%ã%'
or lower(name) like '%á%'
or lower(name) like '%ê%'
or lower(name) like '%é%'
or lower(name) like '%í%'
or lower(name) like '%ú%'
or lower(name) like '%ç%'
) x
inner join
(
select distinct 
	name name_Erro
,	replace(replace(replace(replace(replace(replace(replace(replace(replace(replace(replace(replace(replace(lower(name),'�',''),'ó',''),'õ',''),'ô',''),'á',''),'ã',''),'â',''),'é',''),'ê',''),'í',''),'ú',''),'ç',''),'&','e') name_Sup
from  #categoryNames
where name like '%�%'
) y 
on y.name_Sup = x.name_Sup
;
update salesforce.ctop_es_de_compra
set categoria_site_oferta = replace(categoria_site_oferta ,'&','e')
where categoria_site_oferta like '%&%'
;
update salesforce.ctop_es_de_compra
set Subcategoria_Site_Oferta = replace(Subcategoria_Site_Oferta ,'&','e')
where Subcategoria_Site_Oferta like '%&%'
;
update salesforce.ctop_es_de_compra
set Sub_Subcategoria_Site_Oferta = replace(Sub_Subcategoria_Site_Oferta ,'&','e')
where Sub_Subcategoria_Site_Oferta like '%&%'
;

update salesforce.ctop_es_de_compra 
set categoria_site_oferta = x.name
from #categoryCorrection x
where 
	categoria_site_oferta = x.name_Erro
and categoria_site_oferta like '%�%'
;

update salesforce.ctop_es_de_compra 
set Subcategoria_Site_Oferta = x.name
from #categoryCorrection x
where 
	Subcategoria_Site_Oferta = x.name_Erro
and Subcategoria_Site_Oferta like '%�%'
;
update salesforce.ctop_es_de_compra 
set Sub_Subcategoria_Site_Oferta = x.name
from #categoryCorrection x
where 
	Sub_Subcategoria_Site_Oferta = x.name_Erro
and Sub_Subcategoria_Site_Oferta like '%�%'
;

COMMIT
;

drop table if exists #salesmanager
;
select distinct name 
into #salesmanager
from 
(
select distinct coordenador_de_vendas_equipe name  from salesforce.ctOfertas
)
;

drop table if exists #salesmanagerCorrection

;
SELECT
	name
,	name_erro
into #salesmanagerCorrection
from
(
select distinct 
	replace(name,'&','e') name
,	replace(replace(replace(replace(replace(replace(replace(replace(replace(replace(replace(replace(lower(name),'ó',''),'õ',''),'ô',''),'á',''),'ã',''),'â',''),'é',''),'ê',''),'í',''),'ú',''),'ç',''),'&','e') name_Sup
from  #salesmanager
where lower(name) like '%ó%'
or lower(name) like '%õ%'
or lower(name) like '%ô%'
or lower(name) like '%â%'
or lower(name) like '%ã%'
or lower(name) like '%á%'
or lower(name) like '%ê%'
or lower(name) like '%é%'
or lower(name) like '%í%'
or lower(name) like '%ú%'
or lower(name) like '%ç%'
) x
inner join
(
select distinct 
	name name_Erro
,	replace(replace(replace(replace(replace(replace(replace(replace(replace(replace(replace(replace(replace(lower(name),'�',''),'ó',''),'õ',''),'ô',''),'á',''),'ã',''),'â',''),'é',''),'ê',''),'í',''),'ú',''),'ç',''),'&','e') name_Sup
from  #salesmanager
where name like '%�%'
) y 
on y.name_Sup = x.name_Sup

;
update salesforce.ctOfertas 
set coordenador_de_vendas_equipe = x.name
from #salesmanagerCorrection x
where 
	coordenador_de_vendas_equipe = x.name_Erro
and coordenador_de_vendas_equipe like '%�%'
;

COMMIT
;
