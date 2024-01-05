
1- migration olusturma
DbContextin oldugu projede

dotnet ef migrations add UniqueMigrationName -s ../Akb.Api/

2- migration degisiklerini dbye gecirme
olusan migrationlarin calistirilmasi
sln dizininde 
	dotnet ef database update --project "./Akb.Data" --startup-project "./Akb.Api"

TEST USER ID --> 9041191