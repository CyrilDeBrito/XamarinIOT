# XamarinIOT
Projet on Android Xamarin for IOT dev. 

To download this repo use Git CLI : https://github.com/CyrilDeBrito/XamarinIOT.git

CLI for Git to push only one commit: 
1 - git status
2 - git add *
2.1 - git status (need all green)
3 - git commit -m "your message"
3.1 - git status
4 - git fetch
5 - git rebase origin/master
6 - git push

CLI for Git to push more than one commit: 
1 - git status
2 - git add *
2.1 - git status (need all green)
3 - git commit -m "your message"
3.1 - git status
4 - git fetch
4.1 git log
4.1.1 (you see how many commit)
4.2 git rebase -i HEAD~3 (if you see 3 commit on your branch with git log)
4.2.1 On your text editor you see yours commits messages, they most recent "squash" then. The first (most old) stay in "pick". Save the file.
4.3 git log (you see only one commit (becose you "squash" on 4.2.1)
5 - git rebase origin/master
6 - git push or push -f