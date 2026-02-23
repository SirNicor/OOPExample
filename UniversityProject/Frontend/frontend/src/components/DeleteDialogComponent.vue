<template>
  <el-dialog
      v-model="DeleteDialogVisibility"
      width="500"
      :before-close="close"
  >
    <span>Вы уверены, что хотите удалить? </span>
    <template #footer>
      <div class="dialog-footer">
        <el-button @click="cancel">Закрыть</el-button>
        <el-button type="primary" @click="confirm">Да</el-button>
      </div>
    </template>
  </el-dialog>
</template>

<style scoped>
  
</style>

<script setup lang="ts">
import {ref, watch} from "vue";
import type {TableColumn, TableData} from "@/types/TableTypes.ts";
  const props = defineProps <{
    modelValue : boolean;
  }> ();
  const DeleteDialogVisibility = ref(props.modelValue);
  const emit = defineEmits<
      {
        (e: 'update:modelValue', value: boolean): void
        (e: 'confirm'): void
        (e: 'cancel'): void
      }>();
  const handleClose = (done: () => void) => { DeleteDialogVisibility.value = false;}
  watch(() => props.modelValue, (val) =>
  {
    DeleteDialogVisibility.value = val;
  })
  watch(DeleteDialogVisibility, (val) =>
  {
    emit('update:modelValue', val);
  })
  const cancel = () => {
    DeleteDialogVisibility.value = false
    emit('cancel');
  };
const close = (done : () => void) => {
  DeleteDialogVisibility.value = false
  emit('cancel');
  done();
};
  const confirm = () => {
    DeleteDialogVisibility.value = false
    emit("confirm");
  }
</script>