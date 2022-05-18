
drop table if exists #temp1of;

select 
	ParentId
,	createdDate
,	oldValue
,	NewValue
,	rank() over(partition by ParentId order by createddate) changeOrder
into #temp1of
from salesforce.ctOfertas__History
where field = 'Propriet_rio_Compartilhado__c'
and substring(newValue,1,3) = 'a0W';

drop table if exists #temp2of;

select
	a.ParentId
,	a.newvalue SFOwnerId
,	cast(a.createddate as date) startDate
,	cast(dateadd(day,-1,coalesce(b.createddate,'2101-01-01')) as date) endDate
into #temp2of
from #temp1of a 
left join  #temp1of b 
on a.ParentId = b.ParentId 
and a.changeOrder = b.ChangeOrder - 1;

insert into #temp2of
select
	a.ParentId
,	a.oldvalue SFOwnerId
,	cast(b.createddate as date) startDate
,	cast(a.createddate as date) - 1 endDate
from  #temp1of a
inner join salesforce.ctofertas b 
on a.ParentId = b.id 
where a.changeOrder = 1;

delete from  #temp2of
where enddate < startdate;

delete from reports.SFOfferHistory_Owner;

insert into reports.SFOfferHistory_Owner 
select * FROM  #temp2of;

drop table if exists #temp3of;

select
	offer.id "ParentId",
	offer.propriet_rio_compartilhado "SFOwnerId",
	trunc(offer.createddate) "startDate",
	cast('2100-12-31' as date) "endDate"
into #temp3of
from salesforce.ctofertas offer
where 1 = 1
	and offer.id not in (select parentid from reports.sfofferhistory_owner);

insert into reports.SFOfferHistory_Owner
select * from #temp3of;
