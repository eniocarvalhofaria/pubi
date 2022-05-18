
drop table if exists #temp1;

select 
	AccountId
,	createdDate
,	oldValue
,	NewValue
,	rank() over(partition by AccountId order by createddate,id) changeOrder
into #temp1
from salesforce.sfAccounthistory
where field = 'Propriet_rio_Compartilhado__c'
and substring(newValue,1,3) = 'a0W';

drop table if exists #temp2;

select
	a.accountId
,	a.newvalue SFOwnerId
,	cast(a.createddate as date) startDate
,	cast(dateadd(day,-1,coalesce(b.createddate,'2101-01-01')) as date) endDate
into #temp2
from #temp1 a 
left join  #temp1 b 
on a.accountId = b.accountId 
and a.changeOrder = b.ChangeOrder - 1;

insert into #temp2
select
	a.accountId
,	a.oldvalue SFOwnerId
,	cast(b.createddate as date) startDate
,	cast(a.createddate as date) - 1 endDate
from  #temp1 a
inner join salesforce.sfaccount b 
on a.accountId = b.id 
where a.changeOrder = 1;

delete from  #temp2
where enddate < startdate;

delete from reports.SFAccountHistory_Owner;

insert into reports.SFAccountHistory_Owner 
select * FROM  #temp2;

drop table if exists #temp3;

select
	acc.id "accountid",
	acc.propriet_rio_compartilhado "SFOwnerId",
	trunc(acc3.createddate) "startDate",
	cast('2100-12-31' as date) "endDate"
into #temp3
from salesforce.cfaccount acc
inner join (select acc2.id, acc2.createddate from salesforce.sfaccount acc2) acc3
	on acc3.id = acc.id
where 1 = 1
	and acc.id not in (select accountid from reports.sfaccounthistory_owner);

insert into reports.SFAccountHistory_Owner 
select * FROM  #temp3;

