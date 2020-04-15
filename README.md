# HospitalProjectTeam2

Arun's video demo: https://drive.google.com/file/d/15cOofF8cTAXKKe1Cko6NwUYvIbZ0247L/view?usp=sharing

## Arun Rathinam (n01358379)
1. ### Careers (Job Portal) Feature 
    #### Progress
      - Models
        - Job
        - Applications
        - View Models
          - JobApplications
          - JobsApplications
          
      - Views
        - Job
            - Job/AddJobAdmin
              - Where admin can add a new job
            - Job/ApplyJobAdmin
              - Where the admin can apply for a job (Includes Resume upload)
            - Job/ListJobsAdmin
              - Admin can see list of jobs
              - Navigate to Add/Update/Apply/View pages
              - Delete a particular job
            - Job/ShowJobAdmin 
              - Admin can see list of applications for the particular job
              - Admin can see the job details
              - Admin can download the resume of a particular applicant
              - Admin can reject an application
            - Job/UpdateJobAdmin
              - Admin can update a particular job
            - Job/ListJobs
              - User can see the list of jobs in the hospital
              - User can View a particular job
            - Job/ShowJib
              - User can see the details of a particular job
              - User can apply for a job
            - Job/ApplyJob
              - User can apply for a particular job
              - Resume Upload feature included. 
        - Application
            - Application/AddApplicationAdmin
              - Admin can add a new application
            - Application/ListApplicationsAdmin
              - Admin can see the list of all applications
              - Admin can download the resume of a particular applicant
              - Admin has navigation links to Update/View pages
              - Admin can reject a particular application
            - Application/ShowApplicationAdmin
              - Shows details of a particular application
              - Admin can View/Download resume. 
              - Admin can reject the application
            - Application/UpdateApplicationAdmin
              - Admin can update a particular application
        
          
      - Controller
        - JobController
        - ApplicationsController
2. ### Feedbacks Feature
    #### Progress
      - Models
        - FrequentlyAskedQUestion
      
      - Views
        - FrequentlyAskedQuestion
          - AddFAQAdmin
            - Admin can add a new FAQ
          - ListFAQAdmin
            - Admin can see the list of FAQs
            - Has navigation links to Add,Delete,Update FAQ
          - UpdateFAQAdmin
            - Admin can update the details of a particular FAQ
          - List/FAQ 
            - Users can see the list of FAQs
            
      - Controller
        - FrequentlyAskedQuestionControllerr
        
     
        
