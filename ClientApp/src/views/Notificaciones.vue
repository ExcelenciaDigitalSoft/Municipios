<template>
    <div class="p-4">
      <div class="mb-4">
        <h2 class="text-2xl font-bold">Notificaciones</h2>
        <form @submit.prevent="crearNotificacion" class="grid grid-cols-2 gap-4 mt-4">
          <input v-model="nuevo.celularDestino" placeholder="Celular Destino" class="input" />
          <input v-model="nuevo.canal" placeholder="Canal (WhatsApp, SMS, Email)" class="input" />
          <input v-model="nuevo.fechaEmision" type="date" class="input" />
          <textarea v-model="nuevo.mensaje" placeholder="Mensaje" class="input col-span-2"></textarea>
          <button type="submit" class="btn col-span-2">{{ editar ? 'Actualizar' : 'Crear' }}</button>
        </form>
      </div>
  
      <div class="card mt-4">
        <ul>
          <li v-for="n in notificaciones" :key="n.id" class="py-2 border-b flex justify-between items-center">
            <span>{{ n.canal }} - {{ n.celularDestino }} - {{ n.mensaje }}</span>
            <div class="space-x-2">
              <button class="btn" @click="seleccionarEditar(n)">Editar</button>
              <button class="btn bg-red-500" @click="eliminarNotificacion(n.id)">Eliminar</button>
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
      const notificaciones = ref([]);
      const editar = ref(false);
      const nuevo = ref({
        id: null,
        idContribuyente: 1,
        celularDestino: '',
        canal: '',
        fechaEmision: '',
        mensaje: '',
        enviada: false,
        leida: false
      });
  
      const token = localStorage.getItem('token');
      const headers = { Authorization: `Bearer ${token}` };
  
      const cargarNotificaciones = async () => {
        const res = await axios.get('/api/admin/gestion/notificaciones', { headers });
        notificaciones.value = res.data;
      };
  
      const crearNotificacion = async () => {
        if (editar.value) {
          await axios.put(`/api/notificaciones/${nuevo.value.id}`, nuevo.value, { headers });
        } else {
          await axios.post('/api/admin/gestion/notificaciones', nuevo.value, { headers });
        }
        await cargarNotificaciones();
        limpiarFormulario();
      };
  
      const eliminarNotificacion = async (id) => {
        await axios.delete(`/api/admin/gestion/notificaciones/${id}`, { headers });
        await cargarNotificaciones();
      };
  
      const seleccionarEditar = (n) => {
        Object.assign(nuevo.value, { ...n });
        editar.value = true;
      };
  
      const limpiarFormulario = () => {
        Object.assign(nuevo.value, {
          id: null,
          idContribuyente: 1,
          celularDestino: '',
          canal: '',
          fechaEmision: '',
          mensaje: '',
          enviada: false,
          leida: false
        });
        editar.value = false;
      };
  
      onMounted(() => cargarNotificaciones());
  
      return {
        notificaciones,
        nuevo,
        editar,
        crearNotificacion,
        eliminarNotificacion,
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
  