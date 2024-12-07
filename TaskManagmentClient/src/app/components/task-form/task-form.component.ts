import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Task } from 'src/app/models/task';
import { TaskService } from 'src/app/services/task.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { MatInputModule } from '@angular/material/input';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatButtonModule } from '@angular/material/button';
import Swal from 'sweetalert2'




@Component({
  selector: 'app-task-form',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    RouterModule,
    MatInputModule,
    MatCheckboxModule,
    MatButtonModule],
  templateUrl: './task-form.component.html',
  styleUrls: ['./task-form.component.css']
})
export class TaskFormComponent implements OnInit {
  currentTask: Task = {} as Task;
  id!: string;

  constructor(
    private taskService: TaskService,
    private route: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit(): void {

    this.id = this.route.snapshot.params['id'];
    this.getTask(this.id);

  }

  getTask(id: string): void {
    if (id) {
      this.taskService.getItem(id).subscribe({
        next: (data) => {
          this.currentTask = data as Task;
        },
        error: (e) => console.error(e)
      });
    }
    else {
      this.currentTask = {} as Task
    }
  }

  saveTask(): void {
    if (this.id) {
      this.updateTask();
    }
    else {
      this.createTask();
    }
    this.router.navigate(['/tasks']);
  }

  updateTask(): void {
    this.taskService
      .updateItem(this.currentTask).then((statusCode) => {

        if (statusCode == 200) {
          Swal.fire({
            title: "The Task Updated Succefully",
            icon: "success",
            showConfirmButton: false,
            timer: 2000
          });
        }
      })
        .catch(err => {
          Swal.fire({
            title: "Something went wrong",
            icon: "error",
            showConfirmButton: false,
            timer: 2000
          });
        })
  
  }

  createTask(): void {
    this.taskService.addItem(this.currentTask).then((statusCode) => {

      if (statusCode == 200) {
        Swal.fire({
          title: "A new Task Created Succefully",
          icon: "success",
          showConfirmButton: false,
          timer: 2000
        });
      }
    })
      .catch(err => {
        Swal.fire({
          title: "Something went wrong",
          icon: "error",
          showConfirmButton: false,
          timer: 2000
        });
      })


  }

}

