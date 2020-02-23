![Microsoft Cloud Workshops](../Media/ms-cloud-workshop.png "Microsoft Cloud Workshops")

# Modern Cloud Apps

Before the hands-on lab setup guide

March 2020

## Table Of Contents
- [Modern cloud apps hands-on lab step-by-step](#modern-cloud-apps-hands-on-lab-step-by-step)
  - [Abstract and learning objectives](#abstract-and-learning-objectives)
  - [Overview](#overview)
  - [Solution architecture](#solution-architecture)
  - [Requirements](#requirements)
  - [Help references](#help-references)
  - [Exercise 1: Proof of concept deployment](#exercise-1-proof-of-concept-deployment)
    - [Task 1: Deploy the e-commerce website, SQL Database, and storage](#task-1-deploy-the-e-commerce-website-sql-database-and-storage)
      - [Subtask 1: Configure SQL Database Firewall and Retrieve Connection String](#subtask-1-configure-sql-database-firewall-and-retrieve-connection-string)
      - [Subtask 2: Retrieve Storage Account Access Keys](#subtask-2-Retrieve-Storage-Account-Access-Keys)
      - [Subtask 3: Update the configuration in the starter project](#subtask-3-update-the-configuration-in-the-starter-project)
      - [Subtask 4: Deploy the e-commerce Web App from Visual Studio](#subtask-4-deploy-the-e-commerce-web-app-from-visual-studio)
    - [Task 2: Setup SQL Database Geo-Replication](#task-2-setup-sql-database-geo-replication)
      - [Subtask 1: Add secondary database](#subtask-1-add-secondary-database)
      - [Subtask 2: Failover secondary SQL database](#subtask-2-failover-secondary-sql-database)
      - [Subtask 3: Test e-commerce Web App after Failover](#subtask-3-test-e-commerce-web-app-after-failover)
      - [Subtask 4: Revert Failover back to Primary database](#subtask-4-revert-failover-back-to-primary-database)
      - [Subtask 5: Test e-commerce Web App after reverting failover](#subtask-5-test-e-commerce-web-app-after-reverting-failover)
    - [Task 3: Deploying the Call Center admin website](#task-3-deploying-the-call-center-admin-website)
      - [Subtask 1: Provision the call center admin Web App](#subtask-1-provision-the-call-center-admin-web-app)
      - [Subtask 2: Update the configuration in the starter project](#subtask-2-update-the-configuration-in-the-starter-project)
      - [Subtask 3: Deploy the call center admin Web App from Visual Studio](#subtask-3-deploy-the-call-center-admin-web-app-from-visual-studio)
    - [Task 4: Deploying the payment gateway](#task-4-deploying-the-payment-gateway)
      - [Subtask 1: Provision the payment gateway API app](#subtask-1-provision-the-payment-gateway-api-app)
      - [Subtask 2: Deploy the Contoso.Apps.PaymentGateway project in Visual Studio](#subtask-2-deploy-the-contosoappspaymentgateway-project-in-visual-studio)
    - [Task 5: Deploying the Offers Web API](#task-5-deploying-the-offers-web-api)
      - [Subtask 1: Provision the Offers Web API app](#subtask-1-provision-the-offers-web-api-app)
      - [Subtask 2: Configure Cross-Origin Resource Sharing (CORS)](#subtask-2-configure-cross-origin-resource-sharing-cors)
      - [Subtask 3: Update the configuration in the starter project](#subtask-3-update-the-configuration-in-the-starter-project-1)
      - [Subtask 4: Deploy the Contoso.Apps.SportsLeague.Offers project in Visual Studio](#subtask-4-deploy-the-contosoappssportsleagueoffers-project-in-visual-studio)
    - [Task 6: Update and deploy the e-commerce website](#task-6-update-and-deploy-the-e-commerce-website)
      - [Subtask 1: Update the Application Settings for the Web App that hosts the Contoso.Apps.SportsLeague.Web project](#subtask-1-update-the-application-settings-for-the-web-app-that-hosts-the-contosoappssportsleagueweb-project)
      - [Subtask 2: Validate App Settings are correct](#subtask-2-validate-app-settings-are-correct)

# Modern cloud apps hands-on lab step-by-step

## Abstract and learning objectives

In this hands-on lab, you will be challenged to implement an end-to-end scenario using a supplied sample that is based on Azure App Services, Microsoft Azure Functions, Azure SQL Database, Azure Logic Apps, and related services. The scenario will include implementing compute, storage, workflows, and monitoring, using various components of Microsoft Azure.

Please note that as opposed to the whiteboard design session, the lab is not focused on maintaining PCI compliance and using more advanced security features such as App Service Environment, Network Security Groups, and Application Gateway. The hands-on lab can be implemented on your own, but it is highly recommended to pair up with other members working on the lab to model a real-world experience and to allow each member to share their expertise for the overall solution.

By the end of this hands-on lab, you will have learned how to use several key services within Azure to improve overall functionality of the original solution, and to increase the security and scalability of the new and improved design.

## Overview

The Cloud Workshop: Modern Cloud Apps lab is a hands-on exercise that will challenge you to implement an end-to-end scenario using a supplied sample that is based on Microsoft Azure App Services and related services. The scenario will include implementing compute, storage, security, and scale using various components of Microsoft Azure. The lab can be implemented on your own, but it is highly recommended to pair up with additional team members to more closely model a real-world experience, and to allow members to share their expertise for the overall solution.

## Solution architecture

![A diagram that depicts the various Azure PaaS services for the solution. Azure AD Org is used for authentication to the call center app. Azure AD B2C for authentication is used for authentication to the client app. SQL Database for the backend customer data. Azure App Services for the web and API apps. Order processing includes using Functions, Logic Apps, Queues and Storage. Azure App Insights is used for telemetry capture.](../Media/Screenshots/image2.png "Solution Architecture Diagram")

## Requirements

1. Microsoft Azure subscription
2. Local machine or a virtual machine configured with Visual Studio 2019 Community Edition
3. Twilio account and/or personal cell phone to setup a trial Twilio account

## Help references

| Description | Links |
|:---------|:-------------|
| SQL firewall | <https://azure.microsoft.com/en-us/documentation/articles/sql-database-configure-firewall-settings/> |
| Deploying a Web App | <https://azure.microsoft.com/en-us/documentation/articles/web-sites-deploy/> |
| Deploying an API app | <https://azure.microsoft.com/en-us/documentation/articles/app-service-dotnet-deploy-api-app/> |
| Accessing an API app from a JavaScript client | <https://azure.microsoft.com/en-us/documentation/articles/app-service-api-javascript-client/> |
| SQL Database Geo-Replication overview | <https://azure.microsoft.com/en-us/documentation/articles/sql-database-geo-replication-overview/> |
| What is Azure AD? | <https://azure.microsoft.com/en-us/documentation/articles/active-directory-whatis/> |
| Azure Web Apps authentication | <http://azure.microsoft.com/blog/2014/11/13/azure-websites-authentication-authorization/> |
| View your access and usage reports | <https://msdn.microsoft.com/en-us/library/azure/dn283934.aspx> |
| Custom branding an Azure AD Tenant | <https://msdn.microsoft.com/en-us/library/azure/Dn532270.aspx> |
| Service Principal Authentication | <https://docs.microsoft.com/en-us/azure/app-service-api/app-service-api-dotnet-service-principal-auth> |
| Consumer Site B2C | <https://docs.microsoft.com/en-us/azure/active-directory-b2c/active-directory-b2c-devquickstarts-web-dotnet> |
| Getting Started with Active Directory B2C | <https://azure.microsoft.com/en-us/trial/get-started-active-directory-b2c/> |
| How to Delete an Azure Active Directory | <https://blog.nicholasrogoff.com/2017/01/20/how-to-delete-an-azure-active-directory-add-tenant/> |
| Run performance tests on your app | <http://blogs.msdn.com/b/visualstudioalm/archive/2015/09/15/announcing-public-preview-for-performance-load-testing-of-azure-webapp.aspx> |
| Application Insights Custom Events | <https://azure.microsoft.com/en-us/documentation/articles/app-insights-api-custom-events-metrics/> |
| Enabling Application Insights | <https://azure.microsoft.com/en-us/documentation/articles/app-insights-start-monitoring-app-health-usage/> |
| Detect failures | <https://azure.microsoft.com/en-us/documentation/articles/app-insights-asp-net-exceptions/> |
| Monitor performance problems | <https://azure.microsoft.com/en-us/documentation/articles/app-insights-web-monitor-performance/> |
| Creating a Logic App | <https://azure.microsoft.com/en-us/documentation/articles/app-service-logic-create-a-logic-app/> |
| Logic app connectors | <https://azure.microsoft.com/en-us/documentation/articles/app-service-logic-connectors-list/> |
| Logic Apps Docs | <https://docs.microsoft.com/en-us/azure/logic-apps/logic-apps-what-are-logic-apps> |
| Azure Functions -- create first function | <https://docs.microsoft.com/en-us/azure/azure-functions/functions-create-first-azure-function> |
| Azure Functions docs | <https://docs.microsoft.com/en-us/azure/logic-apps/logic-apps-azure-functions> |

## Exercise 1: Proof of concept deployment

Duration: 60 minutes

Contoso has asked you to create a proof of concept deployment in Microsoft Azure by deploying the web, database, and API applications for the solution as well as validating that the core functionality of the solution works. Ensure all resources use the same resource group previously created for the App Service Environment.

### Task 1: Deploy the e-commerce website, SQL Database, and storage

In this exercise, you will provision a website via the Azure **Web App + SQL** template using the Microsoft Azure Portal. You will then edit the necessary configuration files in the starter project and deploy the e-commerce website.

#### Subtask 1: Configure SQL Database Firewall and Retrieve Connection String

1. Navigate to the Azure Management portal, [http://portal.azure.com](http://portal.azure.com/), using a new tab or instance and login with your lab-provided Azure credentials.

2. Navigate to the **contososports** resource group.

3. Select the **ContosoSportsDB** SQL Database.

    ![The contososports Resource Group blade with the "ContosoSportsDB" highlighted.](../Media/Screenshots/image22.png "Azure Portal")

4. On the **SQL Database** blade, select the **Show database connection strings** link.

    ![On the SQL Database blade, in the left pane, Overview is selected. In the right pane, under Essentials, the Connection strings (Show database connection strings) link is circled.](../Media/Screenshots/image23.png "SQL Database blade")

5. On the **Database connection strings** blade, select and copy the **ADO.NET** connection string. Then, save it in **Notepad** for use later, being sure to replace the placeholders with your username and password with **demouser** and **demo@pass123**, respectively.

    ![In the Database connection strings blade, the ADO.NET connection string is circled.](../Media/Screenshots/image24.png "Database connection strings blade")

6. Go back to the **contososports** resource group blade, and select the **contososports** SQL Server.

    ![The contososports resource group with the contososports sql server highlighted.](../Media/Screenshots/2019-11-15-17-47-46.png "Azure Portal")

7. On the **Overview** screen of the **SQL Server** blade, select **Set server firewall** link at the top.

    ![In the SQL Server Blade, Overview section, the Set server firewall tile is in a box.](../Media/Screenshots/2019-03-31-14-37-31.png "SQL Server Blade, Essentials section")

8. On the **Firewall Settings** blade, specify a new rule named **ALL**, with START IP **0.0.0.0**, and END IP **255.255.255.255**.

    ![Screenshot of the Rule name, Start IP. and End IP fields on the Firewall Settings blade.](../Media/Screenshots/image27.png "Firewall Settings blade")

    >**Note**: This is only done to make the lab easier to do. In production, you do **NOT** want to open your SQL Database to all IP Addresses this way. Instead, you will want to specify just the IP Addresses you wish to allow through the Firewall.

9. Select **Save**.

    ![Screenshot of the Firewall settings Save button.](../Media/Screenshots/2019-04-10-16-00-29.png "Firewall settings Save button")

10. Update progress can be found by selecting the **Notifications** link located at the top of the page.

    ![Screenshot of the Success dialog box, which says that the server firewall rules have been successfully updated.](../Media/Screenshots/2019-04-19-13-39-41.png "Success dialog box")

11. Close all configuration blades.

#### Subtask 2: Retrieve Storage Account Access Keys

1. Go back to the **contososports** blade resource group, and select the **contoso** Storage account.

2. On the **Storage account** blade, scroll down, and, under the **SETTINGS** menu area, select the **Access keys** option.

    ![In the Storage account blade, under Settings, Access keys is circled.](../Media/Screenshots/image35.png "Storage account blade")

3. On the **Access keys** blade, select the copy button by the **Connection String** field in the **key1** section. Paste the value into **Notepad** for later usage. 

    ![In the Access keys blade default keys section, the copy button for the key1 connection string is circled.](../Media/Screenshots/image36.png "Access keys blade, default keys section")

#### Subtask 3: Update the configuration in the starter project

1. Go back to the **contososports** resource group blade.

2. Select the **contosoapp** web app (**App Service** type).

    ![Resources listed for ContosoSports. Web app selected.](../Media/Screenshots/2019-04-19-13-46-40.png "Resources listed for ContosoSports")

3. Copy the web app URL to Notepad.

    - select the **Overview** link.
    - Copy the URL to Notepad for later use. Use the **Copy to clipboard** link.

    ![In the Web App Overview settings, the URL has a box around the link.](../Media/Screenshots/2019-03-22-16-33-05.png "Contoso Web App Overview")

4. On the **App Service** blade, scroll down in the left pane. Under the **Settings** menu, select **Configuration**.

    ![In the App Service blade, under Settings, select Configuration link.](../Media/Screenshots/2019-04-19-16-38-54.png "Configuration link")

5. Add a new **Application setting** with the following values:

   - Key: `AzureQueueConnectionString`

   - Value: Enter the Connection String for the **Azure Storage Account** just created.

    ![In the App settings section for the App Service blade, the new entry for AzureQueueConnectionString is selected.](../Media/Screenshots/image40.png "App settings section")

6. Locate **Connection Strings** section below **Application Settings**.

    ![The Connection Strings section for the App Service blade displays.](../Media/Screenshots/image41.png "Connection Strings section")

7. Add a new **Connection String** with the following values:

   - Name: `ContosoSportsLeague`

   - Value: **Enter the Connection String for the SQL Database just created**.

   - Type: `SQLAzure`

    >**Important**: Ensure you replace the string placeholder values **{your\_username}** **{your\_password\_here}** with the username and password you setup during previously.

    ![The password string placeholder value displays: Password={your\_password\_here};](../Media/Screenshots/image43.png "String placeholder value")

8. Select **Save**.

#### Subtask 4: Deploy the e-commerce Web App from Visual Studio

1. Navigate to the **Contoso.Apps.SportsLeague.Web** project located in the **Web** folder using the **Solution Explorer** of Visual Studio.

    ![In Solution Explorer, under Solution \'Contoso.Apps.SportsLeague\' (7 projects), Web is expanded, and under Web, Contoso.Apps.SportsLeague.Web is selected.](../Media/Screenshots/2019-04-19-14-03-04.png "Solution Explorer")

2. Right-click the **Contoso.Apps.SportsLeague.Web** project, and select **Publish**.

    >**Note**: Don't publish if the configuration does not show your settings. Choose **New Profile** to publish to your Azure subscription.
    > 
    > ![Visual Studio Publish configuration left over from developer. A don't publish message is displayed. There is a box around New Profile link.](../Media/Screenshots/2019-03-22-12-42-48.png "Select New Profile")

3. Choose **Azure App Service** as the publish target, and choose **Select Existing** and then **Create Profile** at the bottom of the wizard.

    ![On the Publish tab, the Microsoft Azure App Service tile is selected, as is the radio button for Select Existing.](../Media/Screenshots/image47.png "Publish tab")

4. If prompted, log on with your Azure Subscription credentials.

    ![App Service Select Existing App Service dialog is displayed. The Sign In link is highlighted](../Media/Screenshots/2019-04-19-14-07-19.png "Azure Sign In")

    >**Note**: If you Sign In and nothing happens, shut down Visual Studio reopen to the solution. Repeat the publishing steps.

5. Select the **Contoso Sports Web App** (with the name you created previously).

    ![Under Subscriptions, under contososports, contososportsweb0 is selected.](../Media/Screenshots/image49.png "Subscriptions")

6. Select **OK**.

7. Select **Publish** to publish the Web application.

    >**Note**: If prompted with a warning about App Service supporting .NET Core 3.0.0, select **OK** to dismiss the warning.
    >
    > ![App Service .NET Core 3.0.0 support warning](../Media/Screenshots/2019-11-15-18-12-21.png "App Service .NET Core 3.0.0 support warning")

8. In the Visual Studio **Output** view, you will see a status that indicates the Web App was published successfully.

    ![Screenshot of the Visual Studio Output view, with the Publish Succeeded message circled.](../Media/Screenshots/image50.png "Visual Studio Output view")

    >**Note**: Your URL will differ from the one shown in the Output screenshot because it must be globally unique.

9. A new browser should automatically open the new web applications. Validate the website by choosing the **Store** link on the menu. You should see product items. If products are returned, then the connection to the database is successful.

    ![Screenshot of the Store link.](../Media/Screenshots/image51.png "Store link")

    >**Troubleshooting**: If the web site fails to show products, go back and double check all your connection string entries and passwords web application settings.

### Task 2: Setup SQL Database Geo-Replication

In this exercise, the attendee will provision a secondary SQL Database and configure Geo-Replication using the Microsoft Azure Portal.

#### Subtask 1: Add secondary database

1. Using a new tab or instance of your browser, navigate to the Azure Management Portal <http://portal.azure.com>.

2. Select **SQL databases** in the navigation menu to the left, and select the name of the SQL Database you created previously.

    ![Screenshot of SQL Databases menu option.](../Media/Screenshots/image52.png "SQL Databases")

3. Under the **SETTINGS** menu area, select **Geo-Replication**.

    ![In the Settings section, Geo-Replication is selected.](../Media/Screenshots/image53.png "Settings section")

4. Select the Azure Region to place the Secondary within.

    ![The Geo-Replication blade has a map of the world with locations marked on it. Under the map, Primary is set to West US, which on the map has a blue checkmark.](../Media/Screenshots/image54.png "Geo-Replication blade")

    The Secondary Azure Region should be the Region Pair for the region the SQL Database is hosted in. Consult <https://docs.microsoft.com/en-us/azure/best-practices-availability-paired-regions> to see which region pair the location you are using for this lab is in.

    >**Note**: If you choose a region that cannot be used as a secondary region, you will not be able to pick a pricing plan. Choose another region.

    ![Wrong geo-replication region selected. Not available options presented.](../Media/Screenshots/2019-03-30-16-05-25.png "Not available options presented.")

5. On the **Create secondary** blade, select **Secondary Type** as **Readable**.

6. Select **Target server** ***Configure required settings***.

    ![the Target server Configure required settings option is selected.](../Media/Screenshots/image55.png "Target server option")

7. On the **New server** blade, specify the following configuration:

   - Server name: **A unique value (ensure the green checkmark appears)**

   - Server admin login: **demouser**

   - Password and Confirm Password: **demo@pass123**

    ![The fields in the New Server blade display with the previously defined settings.](../Media/Screenshots/image56.png "New Server blade")

8. Once the values are accepted in the **New server** blade, choose **Select**.

    ![Screenshot of the Select button.](../Media/Screenshots/image20.png "Select button")

9. On the **Create secondary** blade, select **OK**.

    ![Screenshot of the OK button.](../Media/Screenshots/image57.png "OK button")

    > **Note**: The Geo-Replication will take a few minutes to complete.

10. After the Geo-Replication has finished provisioning, select **SQL Databases** in the navigation menu to the left.

    ![The SQL databases option in the Azure Portal navigation menu](../Media/Screenshots/image52.png "SQL Databases")

11. Select the name of the Secondary SQL Database you just created.

    ![In the list of Databases, the ContosoSportsDB secondary replication role is selected.](../Media/Screenshots/image58.png "Database list")

12. On the **SQL Database** blade, open the **Show database connection strings** link.

    ![On the SQL database blade, in the Essentials blade, the Connection strings (show database connection strings) link is circled.](../Media/Screenshots/image59.png "SQL database blade")

13. On the **Database connection strings** blade, select and copy the **ADO.NET** connection string, and save it in Notepad for use later.

    ![On the Database connection strings blade, ADO.NET tab, the connection string is circled.](../Media/Screenshots/image60.png "Database connection strings blade")

14. On the SQL database blade in the Essentials section, select the SQL Database Server name link.

    ![On the SQL database blade in the Essentials section, the Server name (contososqlserver2.database.windows.net) link is circled.](../Media/Screenshots/image61.png "SQL database blade, Essentials section")

15. On the **SQL Server** blade, select **Set server firewall** at the top.

    ![On the SQL Server blade, at the top, the Set server firewall tile is boxed in red.](../Media/Screenshots/image62.png "SQL Server blade, Essentials section")

16. On the **Firewall Settings** blade, specify a new rule named **ALL**, with START IP **0.0.0.0**, and END IP **255.255.255.255**.

    ![On the Firewall Settings blade, in the New rule section, a new rule has been created with the previously defined settings.](../Media/Screenshots/image27.png "New rule section ")

17. Select **Save**.

    ![Screenshot of the Firewall settings Save button.](../Media/Screenshots/2019-04-10-16-00-29.png "Firewall settings Save button")

18. Update progress can be found by choosing the **Notifications** link located at the top of the page.

    ![Screenshot of the Success dialog box, which says that the server firewall rules have been successfully updated.](../Media/Screenshots/2019-04-19-13-39-41.png "Success dialog box")

19. Close all configuration blades.

#### Subtask 2: Failover secondary SQL database

>**Note**: This subtask is optional.

Since the Replication and Failover process can take anywhere from 10 to 30 minutes to complete, you have the choice to skip Subtask 2 through 5, and go directly to Task 3. However, if you have the time, it is recommended that you complete these steps.

1. Using a new tab or instance of your browser, navigate to the Azure Management Portal <http://portal.azure.com>.

2. In the navigation menu to the left, select **SQL databases**, and select the name of the *primary* SQL Database you created previously.

    ![Screenshot of SQL Databases tile](../Media/Screenshots/image52.png "Azure Portal")

3. On the **Settings** blade, select **Geo-Replication**.

    ![On the Settings blade, under Settings, Geo-Replication is selected.](../Media/Screenshots/image64.png "Settings section")

4. On the **Geo-Replication** blade, select the *secondary* database.

    ![The Geo-Replication blade has a map of the world with locations marked on it. Under the map, Primary is set to West US, which on the map has a blue check mark. Under Secondaries, East US is circled, and displays on the map with a green check mark. A line connects the West Coast (blue) and East Coast (green) check marks.](../Media/Screenshots/image65.png "Geo-Replication blade")

5. Select the **Forced Failover** button.

    ![the Forced Failover button is circled on the Secondary database blade.](../Media/Screenshots/image66.png "Secondary database blade")

6. On the **Forced Failover** prompt, select **Yes**.

    ![On the East US Secondary database blade, in response to the questing asking if you are sure you want to proceed, the Yes button is selected.](../Media/Screenshots/image67.png "Failover prompt")

The failover may take a few minutes to complete. You can continue with the next Subtask modifying the Web App to point to the Secondary SQL Database while the Failover is pending.

#### Subtask 3: Test e-commerce Web App after Failover

1. Once completed, in the Azure Portal, select **SQL databases**, and select the NEW **ContosoSportsDB** secondary.

    ![On the SQL databases blade, under Name, the ContosoSportsDB Secondary replication role is circled.](../Media/Screenshots/image68.png "SQL databases blade")

2. Next, select **Show database connection strings**, and copy it off thereby replacing the user and password.

    ![On the SQL database blade, on the left Overview is selected. On the right, under Essentials, the Connection strings (Show database connection strings) link is circled.](../Media/Screenshots/image69.png "SQL database blade")

3. From the Azure portal, select **Resource Groups**, and select **contososports**.

4. Select the **Web App** created earlier.

5. On the **App Service** blade, scroll down in the left pane, and select **Configuration settings**.

    ![In the App Service blade, under Settings, select Configuration link.](../Media/Screenshots/2019-04-19-16-38-54.png "Configuration link")

6. Scroll down, and locate the **Connection strings** section.

7. Update the **ContosoSportsLeague** Connection String to the value of the **original Secondary Azure SQL Database**.

    ![On the App Service blade, in the Connection strings section, the ContosoSportsLeague connection string is circled.](../Media/Screenshots/image70.png "App Service blade")

    >**Note**: Ensure you replace the string placeholder values **{your\_username}** and **{your\_password\_here}** with the username and password you respectively setup during creation (demouser & demo@pass123).

    ![The password string placeholder value displays: Password={your\_password\_here};](../Media/Screenshots/image43.png "String placeholder values")

8. Select the **Save** button.

9. On the **App Service** blade, select **Overview**.

    ![Screenshot of Overview menu option](../Media/Screenshots/image71.png "App Service blade")

10. On the **Overview** pane, select the **URL** for the Web App to open it in a new browser tab.

    ![On the App Service blade, in the Essentials section, the URL (http;//contososportsweb4azurewebsites.net) link is circled.](../Media/Screenshots/image72.png "App Service blade Essentials section")

11. After the e-commerce Web App loads in Internet Explorer, select **STORE** in the top navigation bar of the website.

    ![On the Contoso sports league website navigation bar, the Store button is circled.](../Media/Screenshots/image73.png "Contoso sports league website navigation bar")

12. Verify the product list from the database displays.

    ![Screenshot of the Contoso store webpage. Under Team Apparel, a Contoso hat, tank top, and hoodie display.](../Media/Screenshots/image74.png "Contoso store webpage")

#### Subtask 4: Revert Failover back to Primary database

1. Using a new tab or instance of your browser, navigate to the Azure Management Portal <http://portal.azure.com>.

2. In the new **SQL databases**, and select the name of the SQL Database you created previously.

    ![Screenshot of SQL Databases menu option.](../Media/Screenshots/image52.png "SQL Databases")

3. On the **Settings** blade, select **Geo-Replication**.

    ![In the Settings section, Geo-Replication is selected.](../Media/Screenshots/image64.png "Settings section")

4. On the **Geo-Replication** blade, select the Secondary database.

    ![The Geo-Replication blade has a map of the world with locations marked on it. Under the map, Primary is set to East US, which on the map has a blue check mark. Under Secondaries, West US is circled, and displays on the map with a green check mark. A line connects the East US (blue) and West US (green) check marks.](../Media/Screenshots/image75.png "Geo-Replication blade")

5. Select the **Forced Failover** button.

    ![The Forced Failover button in the Secondary Database blade is circled.](../Media/Screenshots/image76.png "Secondary Database blade")

6. On the **Forced Failover** prompt, select **Yes**.

    ![On the West US Secondary database blade, in response to the questing asking if you are sure you want to proceed, the Yes button is circled.](../Media/Screenshots/image77.png "Failover prompt")

The failover may take a few minutes to complete. You can continue with the next Subtask modifying the Web App to point back to the Primary SQL Database while the Failover is pending.

#### Subtask 5: Test e-commerce Web App after reverting failover

1. In the Azure Portal, select **Resource Groups** **\>** **contososports** resource group.

2. Select the **Web App** created in a previous step.

3. On the **App Service** blade, scroll down in the left pane, and select **Configuration settings**.

    ![In the App Service blade, under Settings, select Configuration link.](../Media/Screenshots/2019-04-19-16-38-54.png "Configuration link")

4. Scroll down, and locate the **Connection strings** section.

5. Update the **ContosoSportsLeague** Connection String back to the value of the Connection String for the **original Primary SQL Database**.

    ![In the App Service blade Connection strings section, the ContosoSportsLeague connection string is circled.](../Media/Screenshots/image70.png "App Service blade")

    > **Note**: Ensure you replace the string placeholder values **{your\_username}** **{your\_password\_here}** with the username and password you respectively setup during creation (demouser & demo@pass123).

    ![The password string placeholder value displays: Password={your\_password\_here};](../Media/Screenshots/image43.png "String placeholder value")

6. Select **Save**.

    ![the Save button is circled on the App Service blade.](../Media/Screenshots/2019-03-28-05-36-38.png "App Service blade")

7. On the **App Service** blade, select **Overview**.

    ![Overview is selected on the App Service blade.](../Media/Screenshots/image71.png "App Service blade")

8. On the **Overview** pane, select the **URL** for the Web App to open it in a new browser tab.

    ![In the App Service blade Essentials section, the URL (http:/contososportsweb4.azurewebsites.net) link is circled.](../Media/Screenshots/image72.png "App Service blade, Essentials section")

9. After the e-commerce Web App loads in Internet Explorer, select **STORE** in the top navigation bar of the website.

    ![On the Contoso sports league website navigation bar, the Store button is circled.](../Media/Screenshots/image73.png "Contoso sports league website navigation bar")

10. Verify the product list from the database displays.

    ![Screenshot of the Contoso store webpage. Under Team Apparel, a Contoso hat, tank top, and hoodie display.](../Media/Screenshots/image74.png "Contoso store webpage")

### Task 3: Deploying the Call Center admin website

In this exercise, you will provision a website via the Azure Web App template using the Microsoft Azure Portal. You will then edit the necessary configuration files in the Starter Project and deploy the call center admin website.

#### Subtask 1: Provision the call center admin Web App

1. Using a new tab or instance of your browser, navigate to the Azure Management portal <http://portal.azure.com>.

2. Select **+Create a resource** then select **Web**, then **Web App**.

3. Specify a **unique URL** for the Web App, and ensure the **same App Service Plan** and **resource group** you have used throughout the lab are selected. Also, specify **.NET Core 3.0** as the **Runtime stack**.

    ![On the Web App blade, the App name field is set to contososportscallcentercp.](../Media/Screenshots/2019-03-28-05-29-59.png "Web App blade")

4. Select **Windows Plan**, and select the **ContosoSportsPlan** used by the front-end Web app.

5. After the values are accepted, select **Review and create**, then **Create**.  It will take a few minutes to provision.

#### Subtask 2: Update the configuration in the starter project

1. Navigate to the **App Service** blade for the Call Center Admin App just provisioned.

    ![The App Service blade displays.](../Media/Screenshots/image80.png "App Service blade")

2. On the **App Service** blade, select **Configuration** in the left pane.

    ![In the App Service blade, under Settings, select Configuration link.](../Media/Screenshots/2019-04-19-16-38-54.png "Configuration link")

3. Scroll down, and locate the **Connection strings** section.

4. Add a new **Connection string** with the following values:

    - Name: `ContosoSportsLeague`

    - Value: **Enter the Connection String for the primary SQL Database**.

    - Type: `SQL Azure`

    ![The Connection Strings fields display the previously defined values.](../Media/Screenshots/2019-04-11-04-31-51.png "Connection Strings fields")

    >**Note**: Ensure you replace the string placeholder values **{your\_username}** **{your\_password\_here}** with the username and password you respectively setup during creation (demouser & demo@pass123).

    ![The password string placeholder value displays: Password={your\_password\_here};](../Media/Screenshots/image43.png "String placeholder values")

    - Select the **Update** button.

5. Select the **Save** button.

    ![the Save button is circled on the App Service blade.](../Media/Screenshots/2019-03-28-05-36-38.png "App Service blade")

#### Subtask 3: Deploy the call center admin Web App from Visual Studio

1. Navigate to the **Contoso.Apps.SportsLeague.Admin** project located in the **Web** folder using the **Solution Explorer** in Visual Studio.

2. Right-click the **Contoso.Apps.SportsLeague.Admin** project, and select **Publish**.

    ![In Solution Explorer, the right-click menu for Contoso.Apps.SportsLeague.Admin displays, and Publish is selected.](../Media/Screenshots/2019-04-19-14-30-03.png "Right-Click menu")

3. Choose **App Service** as the publish target, choose **Select Existing**, then select **Create Profile**

    ![On the Publish tab, Microsoft Azure App Service is selected. Below that, the radio button is selected for Select Existing.](../Media/Screenshots/image87.png "Publish tab")

4. Select the **Web App** for the Call Center Admin App.

    ![In the App Service section, in the tree view at the bottom, the contososports folder is expanded, and the Call Center Web App is selected.](../Media/Screenshots/image88.png "App Service section")

5. Select **OK**.

6. Select **Publish**.

    ![Display the Visual Studio Contoso.Apps.SportsLeague.Admin publish success message in the output.](../Media/Screenshots/2019-03-28-05-45-28.png "Publish Succeeded")

6. Once deployment is complete, navigate to the Web App. It should look like the following:

    ![The Contoso website displays the Contoso Sports League Admin webpage, which says that orders that display below are sorted by date, and you can select an order to see its details. However, at this time, there is no data available under Completed Orders.](../Media/Screenshots/image89.png "Contoso website")

### Task 4: Deploying the payment gateway

In this exercise, the attendee will provision an Azure API app template using the Microsoft Azure Portal. The attendee will then deploy the payment gateway API to the API app.

#### Subtask 1: Provision the payment gateway API app

1. Using a new tab or instance of your browser, navigate to the Azure Management Portal <http://portal.azure.com>.

2. Select **+Create a resource**, type **API App** into the marketplace search box, and press **Enter**.  Select the **Create** button.

    ![In the Azure Portal left menu, New is selected. In the New blade, the search field is set to API App.](../Media/Screenshots/2019-03-28-07-57-54.png "Azure Portal - Create API App")

3. On the new **API App** blade, create the following values:

   - **App name:** Specify a unique name for the App Name.
   - **Subscription:** Your Azure MSDN subscription.
   - **Resource Group:** select **Use existing** option.
   - **App Service Plan/Location** Select the same primary region used in previous steps.
   - **Application Insights:** **Disabled**

    ![On the API App blade. Configuration fields are displayed.](../Media/Screenshots/2019-04-20-14-55-42.png "Configuration fields are displayed")

4. After the values are accepted, select **Create**.  It will take a few minutes to provision.

#### Subtask 2: Deploy the Contoso.Apps.PaymentGateway project in Visual Studio

1. Navigate to the **Contoso.Apps.PaymentGateway** project located in the **APIs** folder using the **Solution Explorer** in Visual Studio.

2. Right-click the **Contoso.Apps.PaymentGateway** project, and select **Publish**.

    ![In Solution Explorer, Contoso.Apps.PaymentGateway is selected, and in its right-click menu, Publish is selected.](../Media/Screenshots/2019-04-19-14-52-22.png "Solution Explorer")

3. On the **Publish Web** dialog box, select **Azure App Service**, then choose **Select Existing**, and **Create Profile**.

    > **Note**: If your Azure resource group does not show, choose **New Profile**.

4. Select the Payment Gateway API app created earlier, select **OK**.

    ![In the App Service section, the contososports folder is expanded, and PaymentsAPIO is selected. ](../Media/Screenshots/image98.png "App Service section")

5. Select **Publish**.

6. In the Visual Studio **Output** view, you will see a status indicating the Web App was published successfully.

    ![The Visual Studio output shows that the web app was published successfully. ](../Media/Screenshots/image99.png "Visual Studio output")

7. Copy and paste the gateway **URL** of the deployed **API App** into Notepad for later use.

8. Viewing the Web App in a browser will display the Swagger UI for the API.

   ![Payment Gateway is up and running and the Swagger UI is displayed](../Media/Screenshots/2019-04-11-04-58-04.png "Swagger UI")

### Task 5: Deploying the Offers Web API

In this exercise, the attendee will provision an Azure API app template using the Microsoft Azure Portal. The attendee will then deploy the Offers Web API.

#### Subtask 1: Provision the Offers Web API app

1. Using a new tab or instance of your browser, navigate to the Azure Management Portal (<http://portal.azure.com>).

2. Select **+Create a resource**, type **API App** into the marketplace search box, and press **Enter**.  Select the **Create** button.

3. On the new **API App** blade, specify a unique name for the **API App**, and ensure the previously used Resource Group and App Service Plan are selected.

    ![In the API App blade, offersapith is typed in the App name field. App configuration fields displayed.](../Media/Screenshots/2019-04-11-05-03-33.png "API App blade")

4. After the values are accepted, select the **Create** button.

5. When the Web App template has completed provisioning, open the new API App by, in the navigation menu to the left, select **App Services** and then the Offer API app you just created.

   ![In the Azure Portal, on the left More services is selected, and on the right under Web + Mobile, App Services displays.](../Media/Screenshots/image101.png "Azure Portal, More Services")

#### Subtask 2: Configure Cross-Origin Resource Sharing (CORS)

1. On the **App Service** blade for the Offers API, under the **API** menu section, scroll down and select **CORS**.

    ![In the App Service blade, under API, CORS is selected.](../Media/Screenshots/image102.png "App Service blade")

2. In the **Allowed Origins** text box, specify `*` to allow all origins, and select **Save**.

    >**Note**: You should not normally do this in a production environment. In production, you should enter the specific domains as allowed origins you need to allow CORS access to the API. The wildcard (*) is used for this lab to make it easier just for this lab.

    ![CORS configuration blade displayed.  Entering * as the Allowed Origins value.](../Media/Screenshots/2019-03-28-08-20-57.png "CORS configuration blade")

#### Subtask 3: Update the configuration in the starter project

1. On the **App Service** blade for the Offers API, select **Configuration**.

    ![In the App Service blade, under Settings, select Configuration link.](../Media/Screenshots/2019-04-19-16-38-54.png "Configuration link")

2. In the **Connection Strings** section, add a new **Connection string** with the following values:

      - Name: `ContosoSportsLeague`

      - Value: **Enter the Connection String for the SQL Database that was created**.

      - Type: `SQL Azure`

        ![The Connection Strings fields display the previously defined values.](../Media/Screenshots/2019-04-11-04-31-51.png "Connection Strings fields")

        >**Note**: Ensure you replace the string placeholder values **{your\_username}** **{your\_password\_here}** with the username and password you respectively setup during creation (demouser & demo@pass123).

        ![The password string placeholder value displays: Password={your\_password\_here};](../Media/Screenshots/image43.png "String placeholder value")

      - Select the **Update** button.

3. Select the **Save** button.

    ![The Save button is circled on the App Service blade.](../Media/Screenshots/2019-03-28-05-36-38.png "App Service blade")

#### Subtask 4: Deploy the Contoso.Apps.SportsLeague.Offers project in Visual Studio

1. Navigate to the **Contoso.Apps.SportsLeague.Offers** project located in the **APIs** folder using the **Solution Explorer** in Visual Studio.

2. Right-click the **Contoso.Apps.SportsLeague.Offers** project, and select **Publish**.

    ![In Solution Explorer, from the Contoso.Apps.SportsLeague.Admin right-click menu, Publish is selected.](../Media/Screenshots/2019-04-19-15-03-45.png "Solution Explorer")

3. On the **Publish Web** dialog box, select **Azure App Service**, choose **Select Existing**, and select **Create Profile**.

    ![On the Publish tab, the Microsoft Azure App Service tile is selected, and under it, the radio button for Select Existing is selected.](../Media/Screenshots/image109.png "Publish tab")

4. Select the Offers API app created earlier, and select **OK**.

    ![In the App Service section, the contososports folder is expanded, and OffersAPI4 is selected.](../Media/Screenshots/image110.png "App Service section")

5. Select **Publish**.

6. In the Visual Studio **Output** view, you will see a status the API app was published successfully.

7. Record the value of the deployed API app URL into Notepad for later use.

8. Viewing the Web App in a browser will display the Swagger UI for the API.

    ![Payment Gateway is up and running and the Swagger UI is displayed](../Media/Screenshots/2019-04-11-05-20-40.png "Swagger UI")

### Task 6: Update and deploy the e-commerce website

#### Subtask 1: Update the Application Settings for the Web App that hosts the Contoso.Apps.SportsLeague.Web project

1. Using a new tab or instance of your browser, navigate to the Azure Management Portal <http://portal.azure.com>.

2. Select **Resource groups** then the **contososports** resource group.

3. Select the **App Service Web App** for the front-end web application.

    ![In the Resource Group blade on the right, under Name, contosoapp is selected.](../Media/Screenshots/image113.png "Resource Group blade")

4. On the **App Service** blade, scroll down, and select **Configuration** in the left pane.

    ![In the App Service blade, under Settings, select Configuration link.](../Media/Screenshots/2019-04-19-16-38-54.png "Configuration link")

5. Scroll down, and locate the **Applications settings** section.

    ![The App settings section of the App Service blade displays with AzureQueueConnectionString selected.](../Media/Screenshots/image115.png "App settings section")

6. Add a new **Application Setting** with the following values:

   - App Setting Name: `paymentsAPIUrl`

   - Value: Enter the **HTTPS** URL for the Payments API App with `/api/nvp` appended to the end.

        >**Example**: `https://paymentsapi0.azurewebsites.net/api/nvp`

    ![In the Application settings section of the App Service blade, the previously defined application setting values are selected.](../Media/Screenshots/image116.png "App settings")

7. Add another **Application Setting** with the following values:

   - App Setting Name: `offersAPIUrl`

   - Value: Enter the **HTTPS** URL for the Offers API App with `/api/get` appended to the end.

    >**Example**: `https://offersapi4.azurewebsites.net/api/get`

    ![In the Application settings section of the App Service blade, the previously defined application setting values are selected.](../Media/Screenshots/image117.png "App settings section")

    >**Note**: Ensure both API URLs are using **SSL** (https://), or you will see a CORS errors.

8. Select **Save**.

#### Subtask 2: Validate App Settings are correct

1. On the **App Service** blade, select **Overview**.

    ![Overview is selected on the left side of the App Service blade.](../Media/Screenshots/image119.png "App Service blade")

2. In the **Overview** pane, select the **URL** for the Web App to open it in a new browser tab.

    ![On the right side of the App Service blade, under Essentials, the URL (http://contososportsweb2101.azurewebsites.net) link is circled.](../Media/Screenshots/image120.png "App Service blade")

3. On the homepage, you should see the latest offers populated from the Offers API.

    ![On the Contoso Sports League webpage, Today\'s offers display: Baseball socks, Road bike, and baseball mitt.](../Media/Screenshots/image121.png "Contoso Sports League webpage")

4. Submit several test orders to ensure all pieces of the site are functional.  **Accept the default data during the payment processing.**

    ![On the Contoso Sports League webpage, the message Order Completed displays.](../Media/Screenshots/image122.png "Contoso Sports League webpage")

>**Leader Note**: If the attendee is still experiencing CORS errors, ensure the URLs to the Web App in Azure local host are exact.

![Kabel](../Media/Kabel.png "https://www.kabel.es")