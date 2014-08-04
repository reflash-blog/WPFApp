using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CWLOTPH.DB;
using System.Collections.Generic;

namespace CWLOTPH.UnitTesting
{
    [TestClass]
    public class DBInteractionTests
    {
        [TestMethod]
        public void getMaterialTypeTest()
        {
            string mType = "Сталь";

            Material mat_Type = DbInteraсtion.getMaterialType(mType);

            Assert.IsNull(mat_Type);
        }

        [TestMethod]
        public void getAllMaterialTypeTest()
        {
            List<Material> mTlist = DbInteraсtion.getAllMaterialTypes();
            Assert.IsNotNull(mTlist);
        }

        [TestMethod]
        public void getAllUsersTest()
        {
            List<User> uList = DbInteraсtion.getAllUserRecords();
            Assert.IsNotNull(uList);
        }

        [TestMethod]
        public void userTest()
        {
            User user = new User { ID = 1, Login = "demon", Password = "dialup" };
            AdditiveData adata = new AdditiveData { ID = user.ID, Salt = "123456789" };
            User gotUser = DbInteraсtion.getUser("demon");
            if(gotUser==null)
                DbInteraсtion.addUser(user);
            else
            {
                DbInteraсtion.deleteUser(user);
                DbInteraсtion.addUser(user);
            }
            gotUser = DbInteraсtion.getUser("demon");
            Assert.IsNotNull(gotUser);

            DbInteraсtion.deleteUser(user);
            gotUser = DbInteraсtion.getUser("demon");
            Assert.IsNull(gotUser);
        }
    }
}
