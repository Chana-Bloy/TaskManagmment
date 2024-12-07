import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, lastValueFrom } from 'rxjs';
import { Task } from '../models/task';
import { HttpClient } from '@angular/common/http';

const baseUrl = 'https://localhost:7251/api/tasks';

@Injectable({
    providedIn: 'root'
})
export class TaskService {
    private tasksSubject = new BehaviorSubject<Task[]>([]);
    tasksList = this.tasksSubject.asObservable();

    constructor(private http: HttpClient) {
        this.loadTasks()
    }

    // Initialize tasks
    async loadTasks(): Promise<void> {
        try {
            const response = await lastValueFrom(this.http.get<Task[]>(baseUrl));
            this.tasksSubject.next(response || []);
        } catch (error) {
            console.error('Failed to load tasks:', error);
        }
    }

    // Create
    async addItem(item: Task): Promise<number> {
        try {
            const response = await lastValueFrom(this.http.post<Task>(baseUrl, item, { observe: 'response' }));
            this.tasksSubject.next([response.body as Task, ...this.tasksSubject.value]);
            return response.status;
        } catch (error:any) {
            console.error('Failed to add item:', error);
            throw error?.status;
        }
    }

    // Read
    getItems(): Observable<Task[]> {
        return this.tasksList;
    }

    getItem(id: string): Observable<Task> {
        return this.http.get<Task>(`${baseUrl}/${id}`);
    }

    // Update
    async updateItem(updatedItem: Task): Promise<number> {
        try {
            const response = await lastValueFrom(this.http.put<Task>(`${baseUrl}/${updatedItem.id}`, updatedItem, { observe: 'response' }));
            const currentItems = this.tasksSubject.value;
            const updatedItems = currentItems.map(item =>
                item.id === updatedItem.id ? response.body as Task : item
            );
            this.tasksSubject.next(updatedItems);
            return response.status;
        } catch (error:any) {
            console.error('Failed to update item:', error);
            throw error?.status;
        }
    }

    // Delete
    async deleteItem(id: number): Promise<number> {
        try {
            const response = await lastValueFrom(this.http.delete(`${baseUrl}/${id}`, { observe: 'response' }));
            const updatedItems = this.tasksSubject.value.filter(item => item.id !== id);
            this.tasksSubject.next(updatedItems);
            return response.status;
        } catch (error:any) {
            console.error('Failed to delete item:', error);
            throw error.status;
        }
    }
}
