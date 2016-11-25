# Javascript version

https://github.com/Tilerphy/ConcurrentTaskSplitorJs 

# Concurrent Task Splitor
Split A Tasks Group into pairs , then run them concurrently.

For Example:
Jobs: 

`Catch Fish`, `Prepare Fish`, `Cook Fish`, `Cook Pies`, `Make Fire`, `Prepare Pies`.

You know the normal steps maybe:

Catch Fish -> Prepare Fish-> Make Fire -> Cook Fish  then
Prepare Pies -> Make Fire (or use fire made before) -> Cook Pie.

I want to make a model, then the job could be completed by two persons:

one for : Catch Fish -> Prepare Fish -> Find or Make Fire -> Cook Fish 
another for: Prepare Pies -> Find or Make Fire -> Cook Pie

#Let's do it

Let draw a picture:

<img src='http://ww4.sinaimg.cn/large/608f8693jw1f9xhcftgpij20a80a7mxa.jpg'/>

You see that.
`Prepare Pies`, `Fire`, `Found a knife` , `Catch Fish` have no forward arrow.

These four tasks can be started at same time.

When one task is completed, go back and remove itself from the pictures, and `tell` that: I can start next `no forward arrow` task.

If have no `no forward arrow task`, just to wait another work completed.

#What to do?

I have to define some functions now.


1. Find_Tasks_With_No_Forward()

2. Pop_The_Tasks()

3. Peek_The_Tasks()



#How about the data  structure?

```
class TasksManager{
  //List<Worker> works;
  Dictionary<string, TaskItem> AllTasks;
  
}


class TaskItem{
   string uniqueNameOrId;
   List<string> requiredTasks;
   List<string> needTasks;
}
```


#Complete it!
