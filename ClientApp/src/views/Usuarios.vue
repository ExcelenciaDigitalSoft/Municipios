<template>
    <div class="p-4">
      <div class="mb-4">
        <h2 class="text-2xl font-bold">Usuarios</h2>
        <form @submit.prevent="crearUsuario" class="grid grid-cols-2 gap-4 mt-4">
          <input v-model="nuevo.nombreUsuario" placeholder="Nombre de Usuario" class="input" />
          <input v-model="nuevo.email" placeholder="Email" class="input" />
          <input v-model="nuevo.claveHash" placeholder="Contraseña" type="password" class="input" />
          <input v-model="nuevo.nombreCompleto" placeholder="Nombre Completo" class="input" />
          <button type="submit" class="btn col-span-2">{{ editar ? 'Actualizar' : 'Crear' }}</button>
        </form>
      </div>
  
      <div class="card mt-4">
        <ul>
          <li v-for="u in usuarios" :key="u.id" class="py-2 border-b flex justify-between items-center">
            <span>{{ u.nombreUsuario }} - {{ u.email }}</span>
            <div class="space-x-2">
              <button class="btn" @click="seleccionarEditar(u)">Editar</button>
              <button class="btn bg-red-500" @click="eliminarUsuario(u.id)">Eliminar</button>
            </div>
          </li>
        </ul>
      </div>
    </div>
  </template>
  
  <script>
  import { ref, onMounted } from 'vue';
  import axios from 'axios';
  
  export default {
    setup() {
      const usuarios = ref([]);
      const editar = ref(false);
      const nuevo = ref({
        id: null,
        nombreUsuario: '',
        email: '',
        claveHash: '',
        nombreCompleto: '',
        activo: true
      });
  
      const token = localStorage.getItem('token');
      const headers = { Authorization: `Bearer ${token}` };
  
      const cargarUsuarios = async () => {
        const res = await axios.get('/api/admin/gestion/usuarios', { headers });
        usuarios.value = res.data;
      };
  
      const crearUsuario = async () => {
        if (editar.value) {
          await axios.put(`/api/usuarios/${nuevo.value.id}`, nuevo.value, { headers });
        } else {
          await axios.post('/api/admin/gestion/usuarios', nuevo.value, { headers });
        }
        await cargarUsuarios();
        limpiarFormulario();
      };
  
      const eliminarUsuario = async (id) => {
        await axios.delete(`/api/admin/gestion/usuarios/${id}`, { headers });
        await cargarUsuarios();
      };
  
      const seleccionarEditar = (u) => {
        Object.assign(nuevo.value, { ...u });
        editar.value = true;
      };
  
      const limpiarFormulario = () => {
        Object.assign(nuevo.value, {
          id: null,
          nombreUsuario: '',
          email: '',
          claveHash: '',
          nombreCompleto: '',
          activo: true
        });
        editar.value = false;
      };
  
      onMounted(() => cargarUsuarios());
  
      return {
        usuarios,
        nuevo,
        editar,
        crearUsuario,
        eliminarUsuario,
        seleccionarEditar
      };
    }
  };
  </script>
  
  <style scoped>
  .input {
    padding: 0.5rem;
    border: 1px solid #ccc;
    border-radius: 0.5rem;
  }
  .btn {
    background: #2d6cdf;
    color: white;
    padding: 0.5rem;
    border-radius: 0.5rem;
    font-weight: bold;
  }
  .card {
    background: white;
    padding: 1rem;
    border-radius: 1rem;
    box-shadow: 0 2px 4px rgba(0,0,0,0.1);
  }
  </style>