using Microsoft.EntityFrameworkCore;
using binks_forum_API.Models;

namespace binks_forum_API.Data
{
    public class ApplicationDataBaseContext : DbContext
    {
        public ApplicationDataBaseContext(DbContextOptions<ApplicationDataBaseContext> options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Modo> Modos { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<TopicMessages> TopicMessages { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<NewsRank> NewsRanks { get; set; }
        public DbSet<Rank> Ranks { get; set; }
        public DbSet<NewsTopics> NewsTopics { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<AnswerInMessage> AnswersInMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Modèle Document
            modelBuilder.Entity<User>(user =>
            {
                user.ToTable("users");

                user.HasKey(u => u.Id)
                    .HasName("id");
                user.Property(u => u.Id)
                    .HasColumnName("id")
                    .HasField("_id")
                    .IsRequired();
                user.Property(u => u.FirstName)
                    .HasColumnName("firstName")
                    .HasField("_firstName")
                    .IsRequired();
                user.Property(u => u.LastName)
                    .HasColumnName("lastName")
                    .HasField("_lastName")
                    .IsRequired();
                user.Property(u => u.NickName)
                    .HasColumnName("nickName")
                    .HasField("_nickName")
                    .IsRequired();
                user.Property(u => u.Xp)
                    .HasColumnName("xp")
                    .HasField("_xp")
                    .IsRequired();
                user.Property(u => u.RankId)
                    .HasColumnName("rankId")
                    .HasField("_rankId")
                    .IsRequired();
                user.Property(u => u.EncryptedPassword)
                    .HasColumnName("encryptedPassword")
                    .HasField("_encryptedPassword")
                    .IsRequired();
                user.Property(u => u.Country)
                    .HasColumnName("country")
                    .HasField("_country")
                    .IsRequired();
                user.Property(u => u.Mail)
                    .HasColumnName("mail")
                    .HasField("_mail")
                    .IsRequired();
                user.Property(u => u.CreationDate)
                    .HasColumnName("creationDate")
                    .HasField("_creationDate")
                    .IsRequired();
                user.Property(u => u.DoubleAuth)
                    .HasColumnName("doubleAuth")
                    .HasField("_doubleAuth")
                    .HasConversion(
                        v => v ? (byte)1 : (byte)0,
                        v => v == 1
                    )
                    .IsRequired();
                user.Property(u => u.PasswordReset)
                    .HasColumnName("passwordReset")
                    .HasField("_passwordReset")
                    .HasConversion(
                        v => v ? (byte)1 : (byte)0,
                        v => v == 1
                    )
                    .IsRequired();
                user.Property(u => u.ProfileImage)
                    .HasColumnName("profileImage")
                    .HasField("_profileImage")
                    .IsRequired();
                user.Property(u => u.Age)
                    .HasColumnName("age")
                    .HasField("_age")
                    .IsRequired();
                user.Property(u => u.IsShadowBan)
                    .HasColumnName("isShadowBan")
                    .HasField("_isShadowBan")
                    .HasConversion(
                        v => v ? (byte)1 : (byte)0,
                        v => v == 1
                    )
                    .IsRequired();
                user.Property(u => u.FactionId)
                    .HasColumnName("factionId")
                    .HasField("_factionId")
                    .IsRequired();
                user.HasIndex(u => u.Id)
                    .IsUnique()
                    .HasDatabaseName("IX_users_id");
                user.HasIndex(u => u.Mail)
                    .IsUnique()
                    .HasDatabaseName("IX_users_mail");
                user.HasIndex(u => u.FactionId)
                    .IsUnique()
                    .HasDatabaseName("IX_users_faction");
                
            });

            modelBuilder.Entity<Admin> (admin => 
            {
                admin.ToTable("admin");

                admin.Property(a => a.AdminId)
                    .HasColumnName("adminId")
                    .HasField("_adminId")
                    .IsRequired();
                admin.Property(a => a.Id)
                    .HasColumnName("Id")
                    .HasField("_id")
                    .IsRequired();
                admin.HasOne(a => a.User)
                    .WithOne()
                    .HasForeignKey<Admin>(a => a.Id)
                    .HasConstraintName("foreign_admin_users_id")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity<Modo> (modo =>
            {
                modo.ToTable("modo");

                modo.Property(m => m.ModoId)
                    .HasColumnName("modoId")
                    .HasField("_modoId")
                    .IsRequired();
                modo.HasOne(m => m.User)
                    .WithMany()
                    .HasForeignKey(m => m.Id)
                    .HasConstraintName("foreign_modo_users_id")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

            modelBuilder.Entity<Topic> (topic => 
            {
                topic.ToTable("topic");

                topic.HasKey(t => t.TopicId)
                     .HasName("id");
                topic.Property(t => t.TopicId)
                     .HasColumnName("topicId")
                     .HasField("_topicId")
                     .IsRequired();
                topic.Property(t => t.Description)
                     .HasColumnName("description")
                     .HasField("_description")
                     .IsRequired();
                topic.Property(t => t.UserId)
                     .HasColumnName("userId")
                     .HasField("_userId")
                     .IsRequired();
                topic.Property(t => t.Name)
                     .HasColumnName("name")
                     .HasField("_name")
                     .IsRequired();
                topic.Property(t => t.Subjects)
                     .HasColumnName("subjects")
                     .HasField("_subjects")
                     .IsRequired();
                topic.Property(t => t.Views)
                     .HasColumnName("views")
                     .HasField("_views")
                     .IsRequired();
                topic.Property(t => t.Participants)
                     .HasColumnName("participants")
                     .HasField("_participants")
                     .IsRequired();
                topic.Property(t => t.TopicIcon)
                     .HasColumnName("topicIcon")
                     .HasField("_topicIcon");
                topic.Property(t => t.CreationDate)
                     .HasColumnName("creationDate")
                     .HasField("_creationDate")
                     .IsRequired();
                topic.HasOne(t => t.User)
                     .WithMany()
                     .HasForeignKey(t => t.UserId)
                     .HasConstraintName("foreign_topics_users_id")
                     .IsRequired();
                topic.HasIndex(t => t.Name)
                     .IsUnique()
                     .HasDatabaseName("IX_topics_name");
                topic.HasIndex(t => t.TopicIcon)
                     .IsUnique()
                     .HasDatabaseName("IX_topics_topicIcon");
            });

            modelBuilder.Entity<TopicMessages> (topicMessages =>
            {
                topicMessages.ToTable("topicMessages");

                topicMessages.HasKey(t => t.Id)
                             .HasName("id");
                
                topicMessages.Property(t => t.Id)
                     .HasColumnName("Id")
                     .HasField("_id")
                     .ValueGeneratedOnAdd()
                     .IsRequired();
                topicMessages.Property(t => t.TopicId)
                     .HasColumnName("topicId")
                     .HasField("_topicId")
                     .IsRequired();
                topicMessages.Property(t => t.UserId)
                     .HasColumnName("userId")
                     .HasField("_userId")
                     .IsRequired();
                topicMessages.Property(t => t.Date)
                     .HasColumnName("date")
                     .HasField("_date")
                     .IsRequired();
                topicMessages.Property(t => t.Score)
                     .HasColumnName("score")
                     .HasField("_score")
                     .IsRequired();
                topicMessages.Property(t => t.Body)
                     .HasColumnName("body")
                     .HasField("_id")
                     .IsRequired();
                topicMessages.HasOne(t => t.User)
                     .WithMany()
                     .HasForeignKey(t => t.UserId)
                     .HasConstraintName("foreign_topicmessages_users_id")
                     .IsRequired();
                topicMessages.HasOne(t => t.Topic)
                     .WithMany()
                     .HasForeignKey(t => t.TopicId)
                     .HasConstraintName("foreign_topicmessages_topics_id")
                     .IsRequired();
            });

            modelBuilder.Entity<News> (news =>
            {
                news.ToTable("news");

                news.HasKey(n => n.Id)
                    .HasName("id");
                news.Property(n => n.Id)
                    .HasColumnName("id")
                    .HasField("_id")
                    .IsRequired();
                news.Property(n => n.Name)
                    .HasColumnName("name")
                    .HasField("_name")
                    .IsRequired();
                news.Property(n => n.Description)
                    .HasColumnName("description")
                    .HasField("_description")
                    .IsRequired();
                news.Property(n => n.Body)
                    .HasColumnName("body")
                    .HasField("_id")
                    .IsRequired();
                news.Property(n => n.ReleaseDate)
                    .HasColumnName("releaseDate")
                    .HasField("_releaseDate")
                    .IsRequired();           
            });

            modelBuilder.Entity<NewsRank> (newsRank =>
            {
                newsRank.HasKey(nr => nr.Id)
                        .HasName("id");
                newsRank.Property(nr => nr.Id)
                        .HasColumnName("id")
                        .HasField("_id")
                        .IsRequired();
                newsRank.HasOne(nr => nr.News)
                        .WithMany() // Si vous avez ajouté la collection dans News
                        .HasForeignKey(nr => nr.NewsId)
                        .HasConstraintName("foreign_newsranks_news_id")
                        .IsRequired();
                newsRank.HasOne(nr => nr.Rank)
                        .WithMany() // Ou WithMany(r => r.NewsRanks) si Rank a une collection
                        .HasForeignKey(nr => nr.RankId)
                        .HasConstraintName("foreign_newsranks_rank_id")
                        .IsRequired();
            });

            

            modelBuilder.Entity<NewsTopics>(newsTopics =>
            {
                newsTopics.Property(nt => nt.TopicId)
                          .HasColumnName("topicId")
                          .HasField("_topicId")
                          .IsRequired();
                newsTopics.Property(nt => nt.NewsId)
                          .HasColumnName("newsId")
                          .HasField("_newsId")
                          .IsRequired();

                newsTopics.HasOne(nt => nt.News)
                    .WithMany()
                    .HasForeignKey(nt => nt.NewsId)
                    .HasConstraintName("foreign_newstopics_news_id")
                    .IsRequired(); 
                newsTopics.HasOne(nt => nt.Topic)
                    .WithMany(t => t.NewsTopics)
                    .HasForeignKey(nt => nt.TopicId)
                    .HasConstraintName("foreign_newstopics_topics_id")
                    .IsRequired();
            });
                

             
            modelBuilder.Entity<Rank>(rank =>
            {
                rank.HasKey(r => r.Id) // Définir la clé primaire
                    .HasName("id");
                
                rank.Property(r => r.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name")
                    .HasField("_name") // Définir une contrainte sur la longueur
                    .IsRequired();
                rank.Property(r => r.Description)
                    .HasMaxLength(300)// Définir une contrainte sur la longueur
                    .HasColumnName("description")
                    .HasField("_description")
                    .IsRequired(); 
                
                rank.Property(r => r.MinXp)
                    .HasColumnName("minXp")
                    .HasField("_minXp")
                    .IsRequired(); // Définir la propriété comme obligatoire

                rank.Property(r => r.RankIcon)
                    .HasColumnName("rankIcon")
                    .HasField("_rankIcon")
                    .HasMaxLength(200)// Définir une contrainte sur la longueur
                    .IsRequired();
            });

            modelBuilder.Entity<Activity> (activities =>
            {
                activities.HasKey(a => a.Id)
                          .HasName("id");
                activities.Property(a => a.Name)
                          .HasColumnName("name")
                          .HasField("_name")
                          .IsRequired();
                activities.Property(a => a.Description)
                          .HasColumnName("description")
                          .HasField("_description")
                          .IsRequired();
                activities.Property(a => a.CreationDate)
                          .HasColumnName("creationDate")
                          .HasField("_creationDate")
                          .IsRequired();
                activities.Property(a => a.ScheduledDate)
                          .HasColumnName("scheduledDate")
                          .HasField("_scheduledDate")
                          .IsRequired();
                activities.Property(a => a.EndingDate)
                          .HasColumnName("endingDate")
                          .HasField("_endingDate")
                          .IsRequired();
                activities.Property(a => a.Created_by)
                          .HasColumnName("created_by")
                          .HasField("_created_by")
                          .IsRequired();
                activities.Property(a => a.Activity_type)
                          .HasColumnName("activity_type")
                          .HasField("_activity_type")
                          .IsRequired();
                activities.Property(a => a.Is_featured)
                          .HasColumnName("is_featured")
                          .HasField("_is_featured")
                          .IsRequired();
            });

            modelBuilder.Entity<AnswerInMessage>(answersInMessage =>
            {
                answersInMessage.HasKey(a => a.AnswerInMessageId)
                                .HasName("answerInMessageId");
                answersInMessage.Property(a => a.AnswerInMessageId)
                                .HasColumnName("answerInMessageId")
                                .HasField("_answerInMessageId")
                                .IsRequired();
                answersInMessage.Property(a => a.AnswerId)
                                .HasColumnName("answerId")
                                .HasField("_answerId")
                                .IsRequired();
                answersInMessage.Property(a => a.AnsweredMessageId)
                                .HasColumnName("answeredMessageId")
                                .HasField("_answeredMessageId")
                                .IsRequired();
            });
        }
    }
}