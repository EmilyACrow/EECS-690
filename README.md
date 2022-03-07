# EECS-690

## How to use this github:


Create a branch off of develop

```
git checkout -b 'your-name-dev'
```

Do you work, and leave meaningful commit messages

```
git commit -m "Short but meaningful summary of changes"
git push
```

Once your code is ready,  *merge develop into your branch again*

```
git merge develop
```

Resolve any merge conflicts (VS Code is good for this)

Make sure that the code still works - if something is broken, fix it now!

Once merge conflicts are resolved, you can merge your branch into develop

```
git checkout develop
git merge your-branch-name
git push
```
